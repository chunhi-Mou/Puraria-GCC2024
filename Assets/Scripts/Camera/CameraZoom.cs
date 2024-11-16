using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; 
    public float minZoom = 4f;              
    public float maxZoom = 7f;             
    public float zoomSpeed = 5f;                  
    private float targetZoom;                 
    private bool isMinZoom = true;         

    void Start()
    {
        if (virtualCamera.m_Lens.Orthographic)
            targetZoom = virtualCamera.m_Lens.OrthographicSize;
        else
            targetZoom = virtualCamera.m_Lens.FieldOfView;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            targetZoom = isMinZoom ? maxZoom : minZoom;
            isMinZoom = !isMinZoom;
        }

        if (virtualCamera.m_Lens.Orthographic)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetZoom, Time.deltaTime * zoomSpeed);
        }
    }
}
