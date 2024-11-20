using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraSystem : MonoBehaviour {

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] CinemachineConfiner2D confiner;
    private bool isStart = true;
    [Header("Following Player Data")]
    [SerializeField] bool isFollowingPlayer = false;
    [SerializeField] float followingSpeed = 10f;
    [SerializeField] bool isComingBack = false;
    [SerializeField] float comingBackSpeed = 2f;
    [Header("Drag Data")]
    [SerializeField] bool dragPanMoveActive = false;
    [SerializeField] float dragSpeed = 2f;
    [SerializeField] float distanceDragLimit = 30f;
    [Header("Zoom Data")]
    [SerializeField] float minZoom = 6f;
    [SerializeField] float maxZoom = 12f;

    private Vector2 lastMousePosition;
    private void Start() {
        isStart = true;
    }
    private void Update() {
        if (isStart) {
            transform.position = PlayerController.Instance.transform.position;
            isStart = false;
        }
        this.HandleInput();
        this.CameraZoom();
        if ((isFollowingPlayer||isComingBack) && !dragPanMoveActive)
            this.HandleFollowingPlayerCamera();
        if (dragPanMoveActive)
            this.HandleDragToPanCamera();
    }
    private void CameraZoom() {
        if (Input.mouseScrollDelta.y > 0) {
            virtualCamera.m_Lens.OrthographicSize = minZoom;
            confiner.InvalidateCache();
        }
        if (Input.mouseScrollDelta.y < 0) { 
            virtualCamera.m_Lens.OrthographicSize = maxZoom;
            confiner.InvalidateCache();
        }
    }
    private void HandleInput() {
        //Drag
        this.CheckDragLimit();
        if (Input.GetMouseButtonDown(1)) {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
            isComingBack = true;
            isFollowingPlayer = false;
        }

        if (Input.GetMouseButtonUp(1)) {
            dragPanMoveActive = false;
        }
    }
    private void CheckDragLimit() {
        Vector3 targetPosition = PlayerController.Instance.transform.position;
        if (Vector3.Distance(transform.position, targetPosition) > distanceDragLimit)
            dragPanMoveActive = false;
    }
    private void HandleFollowingPlayerCamera() {
        float smoothSpeed = 0f;
        if (isFollowingPlayer)
            smoothSpeed = followingSpeed;
        if (isComingBack)
            smoothSpeed = comingBackSpeed;

        Vector3 targetPosition = PlayerController.Instance.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
       
        if (isComingBack && transform.position == targetPosition) {
            isComingBack = false;
            isFollowingPlayer = true;
        }
    }
    private void HandleDragToPanCamera() {
        Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        // Di chuyển
        Vector3 moveDir = new Vector3(-mouseMovementDelta.x, -mouseMovementDelta.y, 0);
        transform.position += moveDir * dragSpeed * Time.deltaTime;
    }
}
