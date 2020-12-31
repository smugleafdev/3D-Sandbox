using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerPauseManager : MonoBehaviour {

    [HideInInspector]
    public bool gameIsPaused = false;
    GameObject[] pauseObjects;

    void Start() {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePaused();
    }

    void PauseGame() {
        Time.timeScale = 0;
        gameIsPaused = true;
        ShowPaused();
    }

    void ResumeGame() {
        Time.timeScale = 1;
        gameIsPaused = false;
        HidePaused();
    }

    public void Reload() {
        SceneManager.LoadScene("random test");
    }

    public void Quit() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void HandlePause() {
        if (!gameIsPaused) {
            PauseGame();
        } else {
            ResumeGame();
        }
    }

    void ShowPaused() {
        foreach (GameObject obj in pauseObjects) {
            obj.SetActive(true);
        }
    }

    void HidePaused() {
        foreach (GameObject obj in pauseObjects) {
            obj.SetActive(false);
        }
    }
}