using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control : MonoBehaviour
{
    GameObject unit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if(unit==null){
                RaycastHit2D hit = Physics2D.Raycast(pos,pos,0,LayerMask.GetMask("Unit"));
                if(hit) unit=hit.collider.gameObject;
            }
        }
        else {
            RaycastHit2D hit = Physics2D.Raycast(pos,pos,0,LayerMask.GetMask("Unit"));
            if(hit){
                if(hit.collider.gameObject==unit) unit=null;
                else unit=hit.collider.gameObject;
            }
            else {
                hit = Physics2D.Raycast(pos,pos,0,LayerMask.GetMask("Default"));
                if(hit){
                    unit.GetComponent<PlayerController>().path = new List<Vector2>();
                    unit.GetComponent<PlayerController>().currNode=0;
                    unit.GetComponent<PlayerController>().delay=0.5f;
                    unit.GetComponent<PlayerController>().path = GetComponent<PathfindingAStar>().resultPath(unit.transform.position,hit.collider.gameObject.transform.position,3);
                }
            }
        }
        
    }
}
