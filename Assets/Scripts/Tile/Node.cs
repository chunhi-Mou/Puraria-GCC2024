using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float gCost;
    public float hCost;
    public float fCost => gCost + hCost;
    /*
     * public float fCost
    {
        get { return _gCost + _hCost; }
    }
     */
    public Node previousNode;
    public List<Node> neighbors = new List<Node>();
    public bool isObstacle;

}
