using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Node))]
[RequireComponent(typeof(PolygonCollider2D))]
public class SoilBehaviour : MonoBehaviour
{
    private void OnMouseDown() {
            if (!PlayerView.isMoving && !EventSystem.current.IsPointerOverGameObject())
            this.SetAsTarget();
    }

    public void SetAsTarget() {
        Node target = GetComponent<Node>();
        PlayerController.Instance.ReceiveTargetSoil(target);
    }
}
