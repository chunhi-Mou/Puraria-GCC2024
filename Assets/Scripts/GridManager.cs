using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTiles, badLandTiles, _tilePrefab;
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform _player;
    public float xSpacing = 1.0f; // Khoảng cách ngang giữa các prefab
    public float ySpacing = 0.866f; // Khoảng cách dọc giữa các prefab (0.866 = khoảng cách cho hình lục giác đều)
    private Dictionary<Vector2, Tile> _tile;

    void Start()
    {
        GeneralGrid();
    }   

    void GeneralGrid()
    {
        _tile = new Dictionary<Vector2, Tile>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //var _tilePrefab = Random.Range(0, 6) == 3 ? grassTiles : badLandTiles;
                // Kiểm tra xem hàng là chẵn hay lẻ để dịch vị trí
                var _xOffer = y % 2 == 0 ? 1 : xSpacing / 2;
                var spawned = Instantiate (_tilePrefab, new Vector3(x * xSpacing + _xOffer, y * ySpacing * 0.5f, 0), Quaternion.identity);
                spawned.name = $"Tile {x} {y}";

                // Lưu trữ tile vào dictionary
                _tile.Add(new Vector2(x, y), spawned);
            }
        }
        _cam.transform.position = new Vector3(width / 2, height / 4, -10);
        _player.transform.position = new Vector3(width / 2 - 4, height / 4, 0);
    }
     
    public Tile GetTile(Vector2 position)
    {
        if(_tile.TryGetValue(position, out var tile))
        {
            return tile;
        }
        else return null;
    }
}
