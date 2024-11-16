using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cameraToZoom;
    public float zoomSpeed = 2f;
    public float maxZoom = 2f;
    public float minZoom = 10f;
    public float smoothTime = 0.025f;

    private float targetZoom;
    private float zoomVelocity = 0.0f;

    void Start()
    {
        targetZoom = cameraToZoom.orthographicSize;
    }

    void Update()
    {

#if UNITY_EDITOR
        ZoomUsingMouse();
#endif


#if ANDROID || IOS
        ZoomUsingTouch();
#endif
    }


    private void ZoomUsingMouse()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0f)
        {
            targetZoom -= scrollInput * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        }

        cameraToZoom.orthographicSize = Mathf.SmoothDamp(
            cameraToZoom.orthographicSize,
            targetZoom,
            ref zoomVelocity,
            smoothTime
        );
    }


    private void ZoomUsingTouch()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touchDelta = touch2.position - touch1.position;
            float touchDistance = touchDelta.magnitude;

            if (touchDistance != 0)
            {
                targetZoom -= touchDistance * zoomSpeed * Time.deltaTime;
                targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
            }

            cameraToZoom.orthographicSize = Mathf.SmoothDamp(
                cameraToZoom.orthographicSize,
                targetZoom,
                ref zoomVelocity,
                smoothTime
            );
        }
    }
}
