using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.IO.LowLevel.Unsafe;
using TMPro;

public abstract class NodeBase : MonoBehaviour
{
    private Color _obstancleColor;
    [SerializeField] private Gradient _walkableColor;
    [SerializeField] private SpriteRenderer _renderer;

    public ICoords Coords;
    public float GetDistance(NodeBase other)
    {
        return Coords.GetDistance(other.Coords);
    }
    public bool Walkable { get; private set; }
    private bool _selected;
    private Color _defaultColor;

    public virtual void Init(bool walkable, ICoords coords)
    {
        Walkable = walkable;

        _renderer. color = Walkable ? _walkableColor.Evaluate(0) : _obstancleColor;
        _defaultColor = _renderer.color;
        OnHoverTile += OnHoverTile;
        Coords = coords;
        transform.position = Coords.Pos;
        
    }
    public static event Action<NodeBase> OnHoverTile;
    private void OnEnable()
    {
        OnHoverTile += OnHoverTile;
    }
    private void OnDisable()
    {
        OnHoverTile -= OnHoverTile;
    }
    private void OnOnHoverTile(NodeBase tile)
    {
        if (tile == this)
        {
            _selected = true;
        }
        else
        {
            _selected = false;
        }
    }

    private void OnMouseDown()
    {
        if(!Walkable)
        {
            return;
        }
        OnHoverTile?.Invoke(this);
    }
    [Header("PathFinding")]
    [SerializeField] private TextMeshPro _fCostText, _gCostText, _hCostText;

    public List<NodeBase> Neighbors { get; protected set; }
    public NodeBase Connection { get; private set; }
    public float GCost { get; private set; }
    public float HCost { get; private set; }
    public float FCost => GCost + HCost;

    public abstract void CacheNeighbors();

    public void SetConnection(NodeBase node)
    {
        Connection = node;
    }
    public void SetGCost(float cost)
    {
        GCost = cost;
        _gCostText.text = GCost.ToString();
    }
    public void SetHCost(float cost)
    {
        HCost = cost;
        _hCostText.text = HCost.ToString();
    }

    public void SetColor(Color color) => _renderer.color = color;

    public void RevertTile()
    {
        _renderer.color = _defaultColor;
        _gCostText.text = "";
        _hCostText.text = "";
        _fCostText.text = "";
    }


}
public interface ICoords
{
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }

}
