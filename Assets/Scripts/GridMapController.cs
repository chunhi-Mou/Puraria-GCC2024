using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class GridMapController : MonoBehaviour
{
    #region Singleton
    public static GridMapController Instance;
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

    [Header("Grid Map Data")]        
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    [SerializeField] float gap = 0.44f;

    [Header("Rhombus Data")]
    [SerializeField] float side1 = 3.9f;
    [SerializeField] float side2 = 2.42f;
    [SerializeField] float angle = 107.8f;

    [Header("Soil Prefab")]
    [SerializeField] GameObject prefab;

    [Header("other informations")] // them vao

    [SerializeField] private Sprite _playerSprite;
    [SerializeField] private Sprite _goalSprite;
    [SerializeField] private Units _unitPrefab;

    private Node _playerNode, _goalNode;
    private Units _spawnedPlayer, _spawnedGoal;

    private static Node[,] nodes;
    void Start() {
        GenerateMap();
        SpawnUnits();
        Node.OnHoverTile += OnTileHover;
    }

    private void OnDestroy() => Node.OnHoverTile -= OnTileHover;

    // ham xac dinh vi tri click chuot
    private void OnTileHover(Node nodeBase) {
        _goalNode = nodeBase;
        _spawnedGoal.transform.position = _goalNode.transform.position;

        foreach (var t in nodes) t.removeHighlight();
        
        var mc = new PathFindingAStar();
        var path = mc.PlayerGetPath(_playerNode, _goalNode);
        //MovementController.Instance.SetUpPath(path);
        MovementController.Instance.Move(path);
        
        _playerNode = _goalNode;

    }

    public void RemoveHighLight(List<Node> path)
    {
        foreach (var t in path) t.removeHighlight();
    }
    public void GenerateMap()
    {
        //Khởi tạo - Xoá prefab cũ
        GridMapView gridMapView = GetComponent<GridMapView>();
        gridMapView.ClearOldPrefabs();
        nodes = new Node[xSize + 1, ySize + 1];
        
        //Hình thoi
        float angleRad = angle * Mathf.Deg2Rad;

        float xOffset = side1;
        float yOffset = side2 * Mathf.Sin(angleRad);
        float xOffsetY = side2 * Mathf.Cos(angleRad);

        for (int y = 0; y <= ySize; y++) // hàng
        {
            for (int x = 0; x <= xSize; x++) //cột
            {
                //Tính vị trí tâm
                float posX = x * xOffset + y * xOffsetY;
                float posY = y * yOffset - x * gap; //cột sau thấp hơn trước 'gap' đơn vị
                Vector2 position = new Vector2(posX, posY);
                //Tạo Node và Lưu vào Mảng
                nodes[x, y] = gridMapView.CreateNode(x, y, position, prefab);

            }
        }
        UpdateNeighborsNodes();
    }

    private void UpdateNeighborsNodes()
    {
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                nodes[x, y].neighbors = new List<Node>();

                foreach (var dir in Node.directions)
                {
                    int curX = x + (int)dir.x;
                    int curY = y + (int)dir.y;

                    if (curX < 0 || curY < 0) continue;
                    if (curX > xSize || curY > ySize) continue;

                    nodes[x, y].neighbors.Add(nodes[curX, curY]);
                }
            }
        }
    }

    public void ClearPrevNode() {
        foreach (Transform child in transform) {
            Debug.Log(child.name);
            Node nodeOfChild = child.GetComponent<Node>();
            nodeOfChild.prevNode = null;
        }
    }

    void SpawnUnits() {
        _playerNode = nodes[3, 5];
        MovementController.Instance.SetUpStart(_playerNode);
        //_spawnedPlayer = Instantiate(_unitPrefab, _playerNode.transform.position, Quaternion.identity);
        //_spawnedPlayer.Init(_playerSprite);

        _spawnedGoal = Instantiate(_unitPrefab, new Vector3(50, 50, 50), Quaternion.identity);
        //_spawnedGoal.Init(_goalSprite);
        _spawnedGoal.Init(null);
    }


}
