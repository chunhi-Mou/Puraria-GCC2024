using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private float dragSpeed = 0.35f;
    private Vector3 originMousePosition;
    private bool isDragging = false;

    void Update()
    {
        // khi nhận chuột trái
        if (Input.GetMouseButtonDown(0)) 
        {
            isDragging = true;
            
            // lưu điểm cuối của vector di chuyển camera (chuyển tọa độ chuột từ màn hình -> tọa độ thế giới)
            originMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        
        // khi chuột đang được kéo
        if (isDragging)
        {
            // lưu điểm đầu của vector di chuyển camera (chuyển tọa độ chuột từ màn hình -> tọa độ thế giới)
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            // tạo vector di chuyển camera từ điểm đầu -> điểm cuối
            Vector3 offset = currentMousePosition - originMousePosition;

            // di chuyển camera
            transform.position += offset * dragSpeed; 
            // cập nhật giá trị đầu mới (nếu không, vector offset sẽ bị lớn và vị trí của cam di chuyển đến vô cùng)
            originMousePosition = currentMousePosition;
        }

        // khi nhả chuột
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
