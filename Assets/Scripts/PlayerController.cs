using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public List<Vector2> path = new List<Vector2>();
    public int  currNode = 0;
    public float delay =0.5f;

    void Update(){
        if(path.Count>0){
            Debug.Log("Found Path");
            if(delay>=0.5f){
                RaycastHit2D hit = Physics2D.Raycast(path[currNode],path[currNode],0,LayerMask.GetMask("Default"));
                if(hit) MoveTo(hit.collider.gameObject);
                else{
                    Debug.Log("No Tile Found");
                    path = new List<Vector2>();
                    currNode =0;
                    delay=0.5f;
                }
            }
            else delay +=Time.deltaTime;
        }
    }
    void MoveTo(GameObject hex){
        transform.position = hex.transform.position;
        transform.parent = hex.transform;
        delay=0;
    }

}
