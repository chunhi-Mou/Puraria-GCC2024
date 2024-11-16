using UnityEngine;
using Cinemachine;

public class FreeViewMode : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; 
    public Transform playerTransform; 
    public Vector2 mapBoundsMin = new Vector2(0, 0); 
    public Vector2 mapBoundsMax = new Vector2(100, 100); 
    public float dragSpeed = 2.0f;
    public KeyCode toggleFreeViewKey = KeyCode.X;
    public float smoothReturnSpeed = 5.0f; 

    private bool isFreeView = false;
    private Vector3 dragOrigin;
    private Transform cameraTransform;
    private bool isReturningToPlayer = false;

    private Vector3 velocity = Vector3.zero; 

    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera is not assigned!");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        cameraTransform = virtualCamera.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleFreeViewKey))
        {
            isFreeView = !isFreeView;

            if (isFreeView)
            {
                virtualCamera.Follow = null;
                Debug.Log("Free view mode enabled.");
            }
            else
            {
                isReturningToPlayer = true;
                Debug.Log("Free view mode disabled. Returning to player.");
            }
        }

        if (isFreeView && Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
        }

        if (isFreeView && Input.GetMouseButton(1))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 difference = dragOrigin - currentMousePosition;

            Vector3 newPosition = cameraTransform.position;
            newPosition.x += difference.x * dragSpeed * Time.deltaTime;
            newPosition.y += difference.y * dragSpeed * Time.deltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, mapBoundsMin.x, mapBoundsMax.x);
            newPosition.y = Mathf.Clamp(newPosition.y, mapBoundsMin.y, mapBoundsMax.y);

            cameraTransform.position = newPosition;

            dragOrigin = currentMousePosition;
        }

        if (isReturningToPlayer)
        {
            Vector3 targetPosition = playerTransform.position;
            cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition, ref velocity, 1 / smoothReturnSpeed);

            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.1f)
            {
                cameraTransform.position = targetPosition;
                virtualCamera.Follow = playerTransform; 
                isReturningToPlayer = false;
                Debug.Log("Camera has returned to the player.");
            }
        }
    }
}
