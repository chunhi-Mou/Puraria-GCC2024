using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class MovementController : MonoBehaviour {

    #region Singleton
    public static MovementController Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    //private List<Node> playerPath = new List<Node>();
    //private Node startNode = null;

    private Node goalNode;
    // private Units _spawnedPlayer, _spawnedGoal;
    public Rigidbody2D rb;

    [SerializeField] float _moveSpeed = 100f;
    public static bool isMoving = false;
    public static Node currNode = null;

    // public void SetUpPath(List<Node> path)
    // {
    //     playerPath = path;
    // }
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
        GridMapController.Instance.RemoveHighLight(path);
        isMoving = false;
    }
    
    public bool Moved(){
        return !isMoving;
    }
    private void SetupRigidBody() {
        rb = GetComponentInParent<Rigidbody2D>();
        rb.position = currNode.transform.position;
    }

    public void SetUpStart(Node node)
    {
        currNode = node;
        this.transform.position = currNode.transform.position;
    }

    public void SetGoalNode(Node _goalNode)
    {
        goalNode = _goalNode;
    }

    public void Move(List<Node> path){
        if (!isMoving) {
            StartCoroutine(MoveAlongPath(path));  
        }
    }
    void Start() {
        this.SetupRigidBody();
    }
 
    
}