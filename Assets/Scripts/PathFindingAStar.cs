using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class PathFindingAStar : MonoBehaviour
{
    #region Singleton
    public static PathFindingAStar Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Lists in GamePlay")]
    public List<Node> resultPath = new List<Node>();
    public List<Node> frontierNodes = new List<Node>();
    public List<Node> exploredNodes = new List<Node>();

    [Header("Nodes In Gameplay")]
    public Node player; // Node vi tri cua player
    public Node targetNode; // Node dich minh can di toi
    public Node currentNode;
    public Units unit;
    
    private void ClearOldData() {

        this.frontierNodes.Clear();
        this.exploredNodes.Clear();
        this.resultPath.Clear();
        GridMapController.Instance.ClearPrevNode();
    }
    
    //Ham player goi de co danh sach duong di
    public List<Node> PlayerGetPath(Node startNode, Node endNode)
    {
        this.ClearOldData();

        this.player = startNode;
        this.targetNode = endNode;

        this.frontierNodes.Add(startNode);

        if (this.FindPath())
        {
            this.BuildPath(endNode);
            foreach (var t in resultPath)
            {
                t.setHighlight();
            }
            if(resultPath.Count == 1) {
                resultPath.First().removeHighlight(); 
            }
            return resultPath;
        }

        Debug.Log("Cant find Path!");
        return null;
    }
    private bool FindPath()
    {
        if (frontierNodes.Count <= 0)
        {
            Debug.LogError("Zero Block to Move");
            return false;
        }
        if (targetNode == player)
        {
            Debug.LogWarning("Zero Block");

            return true;
        }
        if (frontierNodes.Contains(targetNode))
        {
            Debug.LogWarning("One Block");
            return true;
        }

        while (currentNode != targetNode)
        {
            currentNode = null;
            currentNode = BestNodeCostFrontier();

            if (currentNode == null)
            {
                Debug.LogError("Not found target");
                return false;
            }

            if (targetNode == currentNode)
            {
                Debug.Log("Done");
                return true;
            }

            if (AddedExplored(currentNode))
            {
                AddNeighborsFrontier(currentNode);
            }
            else
            {
                Debug.LogError("Bug Frontier to Explored Node");
            }
        }
        return true;
    }
    private Node BestNodeCostFrontier()
    {
        if (frontierNodes.Count == 0) return null;

        Node bestNode = frontierNodes[0];

        foreach (var node in frontierNodes)
        {
            if (node.FCost < bestNode.FCost || (node.FCost == bestNode.FCost && node.hCost < bestNode.hCost))
            {
                bestNode = node;
            }
        }

        return bestNode;
    }
    private void AddNeighborsFrontier(Node node)
    {
        foreach (var neighbor in node.neighbors)
        {
            if (neighbor.isObstacle == true) continue;
            if (frontierNodes.Contains(neighbor))
            {
                CheckChangeNodePrevious(node, neighbor);
            }
            else
            {
                if (AddFrontier(neighbor)) neighbor.prevNode = node;
                neighbor.gCost = node.gCost + Vector2.Distance(node.transform.position, neighbor.transform.position);
            }
        }
    }
    private void CheckChangeNodePrevious(Node current, Node neighbor)
    {
        float FCostNeighborWithCurrent = current.gCost + Vector2.Distance(current.transform.position, neighbor.transform.position);
        bool checkFCost = FCostNeighborWithCurrent < neighbor.FCost;
        bool checkHCost = (FCostNeighborWithCurrent == neighbor.FCost) && (current.hCost < neighbor.prevNode.hCost);
        if (checkFCost || checkHCost)
        {
            neighbor.prevNode = current;
            neighbor.gCost = FCostNeighborWithCurrent;
        }
    }
    private bool AddFrontier(Node node)
    {
        if (node.isObstacle == true) return false;
        if (!frontierNodes.Contains(node) && !exploredNodes.Contains(node))
        {
            frontierNodes.Add(node);
            return true;
        }
        return false;
    }
    private bool AddedExplored(Node node)
    {
        if (!exploredNodes.Contains(node))
        {
            exploredNodes.Add(node);
            frontierNodes.Remove(node);
            return true;
        }
        return false;
    }
    private void BuildPath(Node endNode) {
        resultPath.Clear();
        Node current = endNode;

        while (current != null) {
            if (!current.isObstacle) {
                resultPath.Add(current);
            }
            current = current.prevNode;
        }

        resultPath.Reverse();
    }

}
