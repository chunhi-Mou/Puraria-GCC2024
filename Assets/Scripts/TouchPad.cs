using UnityEngine;
using UnityEngine.UI;

public class TouchPad : MonoBehaviour
{
    public Camera cameraToZoom;      // Camera cần zoom
    public Button zoomButton;        // Nút UI để kích hoạt zoom
    public float zoomSpeed = 5f;     // Tốc độ zoom
    public float maxZoom = 20f;      // Giới hạn zoom gần nhất
    public float minZoom = 60f;      // Giới hạn zoom xa nhất

    private bool isZooming = false;

    void Start()
    {
        // Đăng ký sự kiện cho nút zoom
        zoomButton.onClick.AddListener(StartZoom);
        zoomButton.onClick.AddListener(StopZoom);
    }

    void Update()
    {
        // Kiểm tra nếu đang zoom
        if (isZooming)
        {
            ZoomIn();
        }
    }

    // Hàm bắt đầu zoom
    public void StartZoom()
    {
        isZooming = true;
    }

    // Hàm dừng zoom
    public void StopZoom()
    {
        isZooming = false;
    }

    // Hàm zoom camera
    private void ZoomIn()
    {
        // Điều chỉnh field of view để zoom
        cameraToZoom.fieldOfView -= zoomSpeed * Time.deltaTime;

        // Giới hạn field of view trong khoảng minZoom và maxZoom
        cameraToZoom.fieldOfView = Mathf.Clamp(cameraToZoom.fieldOfView, maxZoom, minZoom);
    }
}
