using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5f;
    private float minZoom = 35f;
    private float maxZoom = 95f;
    private float normZoom = 75f;

    void Update()
    {
        Zoom();
    }

    private void Zoom ()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        
        if (scrollDelta != 0)
        {
            // phóng to thu nhỏ cam
            Camera.main.fieldOfView -= scrollDelta * zoomSpeed;
            // hàm trả về giá trị ở giữa min max, hoặc max nếu giá trị > max và min nếu giá trị < min
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
        }

        // chỉnh zoom về bình thường
        if (Input.GetKeyDown(KeyCode.Space)) {
            Camera.main.fieldOfView = normZoom;
        }
    }
}
