using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public void PauseGame() {
        Time.timeScale = 0f;
    }
    public void ContinueGame() {
        Time.timeScale = 1f;
    }
}
