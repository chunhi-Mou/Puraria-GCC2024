using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SoilType {
    Fertile,
    Cracked,
    Polluted,
    Radioactive
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
    public List<Node> neighbours;
}
