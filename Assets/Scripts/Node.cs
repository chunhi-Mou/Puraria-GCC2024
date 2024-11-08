using System.Collections;
using System.Collections.Generic;
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
    public bool isObstacle;

    public float gCost;
    public float hCost;
    public float FCost => gCost + hCost;

    public Node prevNode;
    public List<Node> neighbors = new List<Node>();
}
