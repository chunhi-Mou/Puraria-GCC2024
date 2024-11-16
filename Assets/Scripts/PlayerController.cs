using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Singleton
    public static PlayerController Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        this.SetUpStartStatus();
    }
    #endregion

    private List<Node> playerPath = new List<Node>();
    private Node startNode = null;

    [SerializeField] GameObject startSoil;
    //Hàm khởi tạo vị trí ban đầu và hiện tại
    private void SetUpStartStatus() {
        if (startSoil != null)
            startNode = startSoil.GetComponent<Node>();
        else
            Debug.LogWarning("Not found startSoil");
        PlayerView.currNode = startNode;
    }
    //Hàm public được 1 ô Đất gọi khi được click để Gọi hàm tìm đường
    public void ReceiveTargetSoil(Node target) {
        playerPath = PathFindingAStar.Instance.PlayerGetPath(PlayerView.currNode, target);
        PlayerView.Instance.PlayerMovementView(playerPath);
    }
}
