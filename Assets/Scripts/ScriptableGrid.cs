using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableGrid : ScriptableObject
{
    [SerializeField] protected NodeBase _nodePrefab;
    [SerializeField] private int _obstacleWeight = 3;
    public abstract Dictionary<Vector2, NodeBase> GenerateGrid();

    protected bool isObstacle() => Random.Range(1, 20) > _obstacleWeight;
}
