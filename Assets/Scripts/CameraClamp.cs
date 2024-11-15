using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public Camera cameraToClamp;

    [SerializeField] private float minX = -50f; // Minimum X boundary
    [SerializeField] private float maxX = 50f;  // Maximum X boundary
    [SerializeField] private float minY = -50f;  // Minimum Y boundary
    [SerializeField] private float maxY = 50f;   // Maximum Y boundary

    void Update()
    {
        if (cameraToClamp == null)
        {
            cameraToClamp = Camera.main; // Use the main camera if none is assigned
            if (cameraToClamp == null)
            {
                Debug.LogError("Camera to clamp is not assigned, and no main camera found.");
                return;
            }
        }

        // Get the current camera position
        Vector3 cameraPosition = cameraToClamp.transform.position;

        // Clamp the camera position within the defined boundaries
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minX, maxX);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY, maxY);

        // Apply the clamped position back to the camera
        cameraToClamp.transform.position = cameraPosition;
    }
}