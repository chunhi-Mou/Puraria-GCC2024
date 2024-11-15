using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfidingAStart : MonoBehaviour
{
    public static PathfidingAStart Instance;
    public List<Node> resultPath = new List<Node>();
    public List<Node> frontierNode = new List<Node>();
    public List<Node> exploredNode = new List<Node>();
    public Node player; // bắt đầu
    public Node target; // kết thúc
    public Node currentNode; // đường đi hiện tại

    public float timeStepShowResult;
    public float timeStepFindPath; // thời gian tìm đường
    float timeFind;
    bool FindPath()
    {
        timeFind = Time.time;
        if(frontierNode.Count == 0)
        {
            Debug.Log("Không tìm thấy đường đi");
            return false;
        }
        if(frontierNode.Contains(target))
        {
            Debug.Log("Tìm thấy đường đi");
            return true;
        }
        if(target == player)
        {
            Debug.Log("Điểm bắt đầu và kết thúc trùng nhau");
            return true;
        }
        while(currentNode != target)
        {
            currentNode = null;
            currentNode = BestNodeCostFrontier();
            if(currentNode == null)
            {
                Debug.Log("Không tìm thấy đường đi");
                return false;
            }
            if(IsNodeTaarget(currentNode))
            {
                Debug.Log("Tìm thấy đường đi");
                return true;
            }
            if(AddExlored(currentNode))
            {
                AddNeightborsFrontier(currentNode);
            }
            else
            {
                Debug.Log("Không tìm thấy đường đi");
                return false;
            }
        }
        timeFind = Time.time - timeFind;
        return true;    
    }
    Node BestNodeCostFrontier(bool bestSpeed = false)
    {
        if(frontierNode.Count <= 0)
        {
            return null;
        }
        if(bestSpeed)
        {
            return frontierNode.OrderBy(node => node.gCost).First();
        }
        else
        {
            frontierNode = frontierNode.OrderBy(node => node.fCost).ToList();
            return frontierNode.Where(node => node.fCost == frontierNode.First().fCost).OrderBy(node => node.hCost).First();
        }
    }

    bool IsNodeTaarget(Node node)
    {
        return node == target;
    }
    bool AddExlored(Node node)
    {
        if(!exploredNode.Contains(node))
        {
            exploredNode.Add(node);
            return true;
        }
        return false;
    }
    void AddNeightborsFrontier(Node node)
    {
        foreach(Node neighbor in node.neighbors)
        {
            if(frontierNode.Contains(neighbor))
            {
                CheckChangeNodePrevious(node, neighbor);
            }
            else
            {
                if (AddFrontier(neighbor)) 
                {
                    neighbor.previousNode = node;
                }
                neighbor.gCost = node.gCost + Vector2.Distance(node.transform.position, neighbor.transform.position);
            }
        }
    }
    void CheckChangeNodePrevious(Node current, Node neighbor)
    {
        var FCostNeighborWithCurrent = currentNode.gCost + Vector2.Distance(currentNode.transform.position, neighbor.transform.position) + neighbor.hCost;
        bool checkFCost = FCostNeighborWithCurrent < neighbor.fCost;
        bool checkHCoust = (FCostNeighborWithCurrent == neighbor.fCost) && (currentNode.hCost < neighbor.previousNode.hCost);
        if(checkFCost || checkHCoust)
        {
            neighbor.previousNode = current;
            neighbor.gCost = current.gCost + Vector2.Distance(current.transform.position, neighbor.transform.position);
        }
    }
    bool AddFrontier(Node node)
    {
        if(node.isObstacle || exploredNode.Contains(node))
        {
            return false;
        }
        frontierNode.Add(node);
        return true;
    }
}
