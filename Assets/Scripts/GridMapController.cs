using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridMapController : MonoBehaviour {
    #region Singleton
    public static GridMapController Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Grid Map Data")]
    public  int xSize;
    public  int ySize;
    [SerializeField] float gap = 0.44f;

    [Header("Rhombus Data")]
    [SerializeField] float side1 = 3.9f;
    [SerializeField] float side2 = 2.42f;
    [SerializeField] float angle = 107.8f;

    [Header("Soil Prefab")]
    [SerializeField] GameObject prefab;

    public static Node[,] nodes;
    public List<Node> nodesList = new List<Node>();

    public void GenerateMap() {
        //Khởi tạo - Xoá prefab cũ
        GridMapView gridMapView = GetComponent<GridMapView>();
        gridMapView.ClearOldPrefabs();
        nodes = new Node[xSize + 1, ySize + 1];
        nodesList.Clear();

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
                nodesList.Add(nodes[x, y]);
            }
        }
        UpdateNeighborsNodes();
    }

    private void UpdateNeighborsNodes() {
        for (int y = 0; y <= ySize; y++) {
            for (int x = 0; x <= xSize; x++) {
                nodes[x, y].neighbors = new List<Node>();

                foreach (var dir in Node.directions) {
                    int curX = x + (int)dir.x;
                    int curY = y + (int)dir.y;

                    if (curX < 0 || curY < 0) continue;
                    if (curX > xSize || curY > ySize) continue;

                    nodes[x, y].neighbors.Add(nodes[curX, curY]);
                }
            }
        }
    }
    //Hàm public được gọi trước mỗi lần tìm đường mới: Reset hết prevNode của các Node về null
    public void ResetPrevNode() {
        Debug.Log(nodes == null);
        for (int y = 0; y <= ySize; y++) {
            for (int x = 0; x <= xSize; x++) {
                nodes[x, y].prevNode = null;
            }
        }
    }
    //Refill lại mảng bị mất khi Runtime
    private void Start() {
        this.RefillNodesArray();
    }
    public void RefillNodesArray() {
        // Tạo lại mảng dựa trên con của GridMapController
        nodes = new Node[xSize + 1, ySize + 1];
        foreach (Transform child in transform) {
            Node node = child.GetComponent<Node>();
            if (node != null) {
                nodes[node.x, node.y] = node;
            }
        }
    }

    public Node GetNode(Vector2 pos){
        pos.y= Mathf.RoundToInt(pos.y/0.86f);
        if(pos.y %2==0) pos.x-=0.5f;
        return nodes[(int)pos.x,(int)pos.y];
    }

    public Node GetNodeInt(int x, int y){
        return nodes[x,y];
    }
}
