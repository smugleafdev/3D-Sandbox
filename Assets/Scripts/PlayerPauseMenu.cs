using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPauseMenu : MonoBehaviour {

    [HideInInspector]
    public bool gameIsPaused = false;

    // private void Update() {
    //     if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused) {
    //         Debug.Log("asdfsadfsaf");
    //         ResumeGame();
    //     }
    // }

    public void PauseGame() {
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}
