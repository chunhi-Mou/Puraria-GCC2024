using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject light;

    void OnMouseEnter()
    {
        light.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tile clicked");
        }
    }
    
    void OnMouseExit()
    {
        light.SetActive(false);
    }
}
