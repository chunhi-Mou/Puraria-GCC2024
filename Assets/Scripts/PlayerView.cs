using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {
    
    #region Singleton
    public static PlayerView Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    private Rigidbody2D rb;

    [SerializeField] float _moveSpeed = 1.0f;

    public static bool isMoving = false;
    public static Node currNode = null;

    public bool PlayerMovementView(List<Node> path) {
        if (!isMoving) {
            StartCoroutine(MoveAlongPath(path));
            return false;
        }
        return true;
    }

    private IEnumerator MoveAlongPath(List<Node> path) {
        isMoving = true;

        foreach (var node in path) {
            currNode = node;
            Vector2 targetPosition = node.transform.position;
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f) {
                Vector2 newPosition = Vector2.MoveTowards(rb.transform.position, targetPosition, _moveSpeed * Time.deltaTime);
                rb.MovePosition(newPosition);
                yield return null;
            }
        }
        isMoving = false;
    }

    private void SetupRigidBody() {
        rb = GetComponentInParent<Rigidbody2D>();
        rb.position = currNode.transform.position;
    }
    private void Start() {
        this.SetupRigidBody();
    }
}