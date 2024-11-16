using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Camera cameraToZoom;
    public float zoomSpeed = 2f;
    public float maxZoom = 2f;
    public float minZoom = 10f;
    private bool isZoomingIn = false;
    private bool isZoomingOut = false;

    public bool isOut = false;

    void Update()
    {
        if (isZoomingIn)
        {
            ZoomIn();
        }
        else if (isZoomingOut)
        {
            ZoomOut();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isOut)
        {
            StartZoomIn();
        }
        else
        {
            StartZoomOut();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopZoom();
    }

    public void StartZoomIn()
    {
        isZoomingIn = true;
        isZoomingOut = false;
    }

    public void StartZoomOut()
    {
        isZoomingOut = true;
        isZoomingIn = false;
    }

    public void StopZoom()
    {
        isZoomingIn = false;
        isZoomingOut = false;
    }

    private void ZoomIn()
    {
        cameraToZoom.orthographicSize -= zoomSpeed * Time.deltaTime;
        cameraToZoom.orthographicSize = Mathf.Clamp(cameraToZoom.orthographicSize, maxZoom, minZoom);
    }

    private void ZoomOut()
    {
        cameraToZoom.orthographicSize += zoomSpeed * Time.deltaTime;
        cameraToZoom.orthographicSize = Mathf.Clamp(cameraToZoom.orthographicSize, maxZoom, minZoom);
    }
}
