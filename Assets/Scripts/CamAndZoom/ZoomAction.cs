using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Camera cameraToZoom;
    public float zoomSpeed = 2f;
    public float maxZoom = 2f;
    public float minZoom = 10f;
    public float smoothTime = 0.025f;
    private bool isZoomingIn = false;
    private bool isZoomingOut = false;
    private float targetZoom;
    private float zoomVelocity = 0.0f;

    void Start()
    {
        targetZoom = cameraToZoom.orthographicSize;
    }

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

        cameraToZoom.orthographicSize = Mathf.SmoothDamp(
            cameraToZoom.orthographicSize,
            targetZoom,
            ref zoomVelocity,
           smoothTime
        );
    }

    public bool isOut = false;
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
        isZoomingIn = false;
        isZoomingOut = false;
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

    private void ZoomIn()
    {
        targetZoom -= zoomSpeed * Time.deltaTime;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
    }

    private void ZoomOut()
    {
        targetZoom += zoomSpeed * Time.deltaTime;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
    }
}
