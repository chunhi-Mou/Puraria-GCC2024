using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapView : MonoBehaviour
{
    public void ClearOldPrefabs()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
    public Node CreateNode(int x, int y, Vector2 position, GameObject prefab)
    {
        if (prefab != null)
        {
            GameObject tile = Instantiate(prefab, position, Quaternion.identity, transform);

            tile.name = "Đất" + "(" + x + ", " + y + ")";
            Node node = tile.GetComponent<Node>();

            Renderer renderer = tile.GetComponentInChildren<SpriteRenderer>();
            
            if (renderer != null)
            {
                renderer.sortingOrder = -(int)(y * 1000) + x;
            }
            return node;
        }
        return null;
    }
}
