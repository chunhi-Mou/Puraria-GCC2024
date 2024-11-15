using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Tarodev_Pathfinding._Scripts
{
    public static class Pathfinding
    {
        private static readonly Color PathColor = new Color(0.65f, 0.35f, 0.35f);
        private static readonly Color OpenColor = new Color(.4f, .6f, .4f);
        private static readonly Color ClosedColor = new Color(0.35f, 0.4f, 0.5f);

        public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode)
        {
            var toSearch = new List<NodeBase>() { startNode };
            var processed = new List<NodeBase>();

            while (toSearch.Any())
            {
                var current = toSearch[0];
                foreach (var t in toSearch)
                    if (t.FCost < current.FCost || t.FCost == current.FCost && t.HCost < current.HCost) current = t;

                processed.Add(current);
                toSearch.Remove(current);

                current.SetColor(ClosedColor);

                if (current == targetNode)
                {
                    var currentPathTile = targetNode;
                    var path = new List<NodeBase>();
                    var count = 100;
                    while (currentPathTile != startNode)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0) throw new Exception();
                        Debug.Log("sdfsdf");
                    }

                    foreach (var tile in path) tile.SetColor(PathColor);
                    startNode.SetColor(PathColor);
                    Debug.Log(path.Count);
                    return path;
                }

                foreach (var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t)))
                {
                    var inSearch = toSearch.Contains(neighbor);

                    var costToNeighbor = current.GCost + current.GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighbor.GCost)
                    {
                        neighbor.SetGCost(costToNeighbor);
                        neighbor.SetConnection(current);

                        if (!inSearch)
                        {
                            neighbor.SetHCost(neighbor.GetDistance(targetNode));
                            toSearch.Add(neighbor);
                            neighbor.SetColor(OpenColor);
                        }
                    }
                }
            }
            return null;
        }
    }
}
