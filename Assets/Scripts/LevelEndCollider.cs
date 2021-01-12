using UnityEngine;

public class LevelEndCollider : MonoBehaviour {

    [SerializeField] GameObject player, platform;
    LevelController levelController;

    private void Start() {
        levelController = transform.GetComponentInParent<LevelController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            ResetLevel();
        }
    }

    void ResetLevel() {
        ResetPlatformAndPlayer();
        levelController.ResetLevel();
    }

    void ResetPlatformAndPlayer() {
        player.transform.position = new Vector3(24.62f, 2.4f, 0);
        platform.transform.position = new Vector3(24.62f, 2.4f, 0);
    }
}