using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{

    public GameObject tilePrefab;
    private const float TILE_HORIZONTAL = 10;

    private const float TILE_VERTICAL = 1.1f;

    private const float TILE_EDGE = 1f;

    private float _disHori = TILE_HORIZONTAL / 2;
    private float _disVert = (TILE_VERTICAL + TILE_EDGE) / 2;

    Vector2 disTileHori = new Vector2(TILE_HORIZONTAL + TILE_EDGE, 0);

    private void Init()
    {
        for (int row = 0; row < 30; row++)
        {
            InitRow(new Vector2((TILE_HORIZONTAL + TILE_EDGE) * (row % 2) / 2, TILE_VERTICAL * row / 2), row);
        }
    }


    private void InitRow(Vector2 dis, int row)
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(tilePrefab, disTileHori * i + dis, tilePrefab.transform.rotation);
            go.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -row;
            go.transform.SetParent(transform);
        }
    }

    private void Start()
    {
        Init();
    }

}