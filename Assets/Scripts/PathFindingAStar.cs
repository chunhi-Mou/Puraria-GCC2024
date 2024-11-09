using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    #region Show A* Algorithm Details
    [Header("List in Gameplay")]
    [SerializeField] List<Node> resultPath = new List<Node>();
    [SerializeField] List<Node> frontierNodes = new List<Node>();
    [SerializeField] List<Node> exploredNodes = new List<Node>();

    [Header("Nodes in Gameplay")]
    [SerializeField] Node player;
    [SerializeField] Node target;
    [SerializeField] Node currentNode;

    [Header("Time")]
    [SerializeField] float timeFind;
    #endregion
    
    //Hàm public Player sẽ gọi để có danh sách Đường đi
    public List<Node> PlayerGetPath(Node startNode, Node endNode)
    {
        this.player = startNode;
        this.target = endNode;

        this.frontierNodes.Add(startNode);

        if (this.FindPath())
        {
            this.BuildPath(endNode);
            return resultPath;
        }

        Debug.Log("Cant find Path!");
        return null;
    }
    private bool FindPath()
    {
        timeFind = Time.time;
        if (frontierNodes.Count <= 0)
        {
            Debug.LogError("Zero Block to Move");
            return false;
        }
        if (frontierNodes.Contains(target))
        {
            Debug.LogWarning("One Block");
            return true;
        }
        if (target == player)
        {
            Debug.LogWarning("Zero Block");
            return true;
        }
        while (currentNode != target)
        {
            currentNode = null;
            currentNode = BestNodeCostFrontier();

            if (currentNode == null)
            {
                Debug.LogError("Not found target");
                return false;
            }

            if (target == currentNode)
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
        timeFind = Time.time - timeFind;
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
    private void BuildPath(Node endNode)
    {
        resultPath.Clear();
        Node current = endNode;

        while (current != null)
        {
            if (!current.isObstacle)
            {
                resultPath.Add(current);
            }
            current = current.prevNode;
        }

        resultPath.Reverse();
    }
}