using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
enum SoilType
{
    Good, //Đất tốt
    Cracked, //Đất nứt nẻ
    Polluted, // Đất Ô nhiễm
    Radioactive // Đất phóng xạ
}
public class Node : MonoBehaviour
{
    public static readonly Vector2[] directions = new Vector2[]
    {
        new Vector2(1, 0),  // Phải
        new Vector2(-1, 0), // Trái
        new Vector2(-1, -1), //Dưới-Trái
        new Vector2(1, 1), // Trên-Phải
        new Vector2(0, 1), // Trên-Trái
        new Vector2(0, -1) //Dưới-Phải
    };

    public bool isObstacle;
    public int soilType;
    public float gCost;
    public float hCost;
    public float FCost;
    public Vector2 pos; 
    public Node prevNode = null;
    public List<Node> neighbors = new List<Node>();
    //Toạ độ x, y của ô đất
    public int x, y;
    public Node (Vector2 pos){
        this.pos=pos;
    }
}
