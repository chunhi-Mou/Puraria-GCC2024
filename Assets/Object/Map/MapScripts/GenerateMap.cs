using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject prefabSoil;
    private int numberOfSoils = 20;
    private float offsetXy = -1f;
    private float offsetXx = 4f;
    private float offsetYy = 2.5f;
    private float offsetYx = 0.5f;
    private int offsetLayer = -500;
    private static Node[,] nodes;
    void Start()
    {
        nodes = new Node [numberOfSoils, numberOfSoils];
        CreateMap(); // cài đặt map mới
    }

    public void CreateMap() 
    {
        DestroyMap(); // xóa map cũ
        Generate(); // tạo map
        FindNeighbourNodes(); // tìm các node kề 
    }

    private void Generate () 
    {
        for (int y = 0; y < numberOfSoils; y++) 
        {
            for (int x = 0; x < numberOfSoils; x++) 
            {
                nodes[x, y] = CreateNode(x, y, prefabSoil); // tạo node mới (tạo đất và cập nhật node)
            }
        }    
    }

    private Node CreateNode (int x, int y, GameObject prefabSoil) 
    {
        if (prefabSoil != null)
        {
            float newPosX = x * offsetXx + y * offsetXy; // vị trí x của đất mới
            float newPosY = - x * offsetYx +  y * offsetYy; // vị trí y của đất mới
            Vector3 newSoilPosition = new Vector3 (newPosX, newPosY); // vị trí của node mới

            GameObject newSoil = Instantiate (prefabSoil, newSoilPosition, Quaternion.identity, transform); // tạo đất mới
            newSoil.name = "soil(" + x + ", " + y +")"; // đặt tên

            SpriteRenderer spriteRenderer = newSoil.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) 
            {
                spriteRenderer.sortingOrder = offsetLayer * y + x; // sắp xếp đất thứ tự hiển thị
            }

            Node newNode = newSoil.GetComponent<Node>(); // lấy node của đất
            return newNode; // trả về node mới
        }
        return null;
    }

    private void FindNeighbourNodes () 
    {
        // duyệt qua tất cả vị trí
        for (int y = 0; y < numberOfSoils; y++) 
        {
            for (int x = 0; x < numberOfSoils; x++)
            {
                // khởi tạo list node bạn
                nodes[x, y].neighbours = new List<Node>();

                foreach (var dir in Node.directions) 
                {
                    // tìm các vị trí x, y liền kề của node theo hướng
                    int posX = x + (int)dir.x;
                    int poxY = y + (int)dir.y;
                    
                    // trường hợp node ở rìa -> không có node bạn
                    if (posX < 0 || poxY < 0) continue;
                    if (posX >= numberOfSoils || poxY >= numberOfSoils) continue;

                    nodes[x, y].neighbours.Add(nodes[posX, poxY]);
                }  
            }
        }
    }

    private void DestroyMap () 
    {
        while (transform.childCount != 0) {
            foreach (Transform child in transform) 
            {
                DestroyImmediate (child.gameObject);
            }
        }
    }
}
