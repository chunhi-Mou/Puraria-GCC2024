using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
[CreateAssetMenu(fileName = "New Hex Grid", menuName = "Scriptable Grids/Hex Grid")]
public class HexaGrid : ScriptableGrid
{
    [SerializeField] private int _gridWeight = 3;
    [SerializeField] private int _gridDepth = 10;

    public override Dictionary<Vector2, NodeBase> GenerateGrid()
    {
        var tiles = new Dictionary<Vector2, NodeBase>();
        var grid = new GameObject("Grid");
        for (int i = 0; i < _gridDepth; i++)
        {
            
            for (int j = 0; j < _gridWeight; j++)
            {
                var tile = Instantiate(_nodePrefab, grid.transform);
                tile.Init(isObstacle(), new HexCoords(j,i));
            }
        }
        return tiles;
    }
}
