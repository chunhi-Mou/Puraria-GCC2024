using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilBehaviour : MonoBehaviour
{
    private void OnMouseDown() {
        if(!PlayerView.isMoving)
            this.SetAsTarget();
        Debug.Log(PlayerView.isMoving);
    }

    public void SetAsTarget() {
        Node target = GetComponent<Node>();
        PlayerController.Instance.ReceiveTargetSoil(target);
    }
}
