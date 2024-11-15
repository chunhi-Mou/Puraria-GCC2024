using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    public float panSpeed = 0.015f;
    public float smoothTime = 0.1f;

    private Vector3 lastMousePosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    public Vector2 panLimitr = new Vector2(22, 9); // giới hạn camera di chuyển bên phải
    public Vector2 panLimitl = new Vector2(11, 5); // giới hạn camera di chuyển bên trái

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition; // lưu giá trị chuột khi lần đầu ấn
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition; // Khoảng cách của chuột hiện dại và chuột cũ
            lastMousePosition = Input.mousePosition; // Cập nhật giá trị chuột cũ

            Vector3 move = new Vector3(-mouseDelta.x * panSpeed, -mouseDelta.y * panSpeed, 0); // Tạo vector di chuyển
            targetPosition += move; // Cập nhật vị trí mới

            targetPosition.x = Mathf.Clamp(targetPosition.x, panLimitl.x, panLimitr.x); // Hàm giới hạn giá trị x
            targetPosition.y = Mathf.Clamp(targetPosition.y, panLimitl.y, panLimitr.y); // Hàm giới hạn giá trị y
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); // hàm di chuyển
    }
}
