using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

enum SoilType
{
    Good, //Đất tốt
    Cracked, //Đất nứt nẻ
    Polluted, // Đất Ô nhiễm
    Radioactive // Đất phóng xạ
}
public class Node : MonoBehaviour
{
    private static readonly Color defautColor = new Color(141, 141, 141, 141);
    private static readonly Color pathColor = new Color(255, 255, 255, 255);
    public static readonly Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0),  // Phải
        new Vector2(-1, 0), // Trái
        new Vector2(-1, -1), //Dưới-Trái
        new Vector2(1, 1), // Trên-Phải
        new Vector2(0, 1), // Trên-Trái
        new Vector2(0, -1) //Dưới-Phải
    };
    //[SerializeField] private SpriteRenderer render;
    [SerializeField] private GameObject highLight, highLight2;

    void Start(){
        highLight2.SetActive(false);
    }
    private void OnMouseEnter() {
        Debug.Log("Mouse touched");
        highLight2.SetActive(true);
    }
        
    private void OnMouseExit() {
        highLight2.SetActive(false);
    }

    public void setHighlight(){
        highLight.SetActive(true);
    }

    public void removeHighlight(){
        highLight.SetActive(false);
    }

    [Header("Node Information")]
    public bool isObstacle;

    public float gCost;
    public float hCost;
    public float FCost => gCost + hCost;

    public Node prevNode;
    public List<Node> neighbors = new List<Node>();

    //Duong di chuyen
    private bool _selected;
    public static event Action<Node> OnHoverTile;
    private void OnEnable() => OnHoverTile += OnOnHoverTile;
    private void OnDisable() => OnHoverTile -= OnOnHoverTile;
    private void OnOnHoverTile(Node selected) => _selected = selected == this;

    protected virtual void OnMouseDown() {
        if (isObstacle) return;
        if(MovementController.Instance.Moved()) OnHoverTile?.Invoke(this);
    }
}
