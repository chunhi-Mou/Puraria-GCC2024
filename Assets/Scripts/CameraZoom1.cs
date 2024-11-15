using UnityEngine;

public class CameraZoom1 : MonoBehaviour
{
    [SerializeField] private Camera cameraToZoom;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float maxZoom = 20f; // Minimum FOV value for a close zoom
    [SerializeField] private float minZoom = 60f; // Maximum FOV value for a far zoom
    [SerializeField] private float smoothTime = 0.025f;

    private float targetZoom;
    private float zoomVelocity = 0.0f;

    void Start()
    {
        // Ensure cameraToZoom is assigned
        if (cameraToZoom == null)
        {
            cameraToZoom = Camera.main; // Try to default to the main camera
            if (cameraToZoom == null)
            {
                Debug.LogError("Camera to zoom is not assigned, and no main camera found.");
                return;
            }
        }

        targetZoom = cameraToZoom.fieldOfView; // Initialize targetZoom with the current field of view
    }

    void Update()
    {
        if (cameraToZoom == null) return; // Prevent updates if no camera is set

#if UNITY_EDITOR
        ZoomUsingMouse();
#endif

#if UNITY_ANDROID || UNITY_IOS
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

        cameraToZoom.fieldOfView = Mathf.SmoothDamp(
            cameraToZoom.fieldOfView,
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

            float prevTouchDeltaMag = (touch1.position - touch1.deltaPosition).magnitude - (touch2.position - touch2.deltaPosition).magnitude;
            float touchDeltaMag = (touch1.position - touch2.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            targetZoom += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;
            targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);

            cameraToZoom.fieldOfView = Mathf.SmoothDamp(
                cameraToZoom.fieldOfView,
                targetZoom,
                ref zoomVelocity,
                smoothTime
            );
        }
    }
}
