using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScript : MonoBehaviour
{
    private CameraHandle cameraHandle;
    private CameraZoom cameraZoom;
    private Tile tile;
    private void Start()
    {
        cameraHandle = GameObject.Find("Main Camera").GetComponent<CameraHandle>();
        cameraZoom = GameObject.Find("Main Camera").GetComponent<CameraZoom>();
        //tile = GetComponent<Tile>();
    }
    public void FalseeSettingCamera()
    {
        
        cameraHandle.enabled = false;
        cameraZoom.enabled = false;
        //tile.enabled = false;
    }
    public void TrueeSettingCamera()
    {
        cameraHandle.enabled = true;
        cameraZoom.enabled = true;
        //tile.enabled = true;
    }
}
