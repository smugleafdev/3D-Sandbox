using UnityEngine;

public class LevelEndCollider : MonoBehaviour {

    [SerializeField] GameObject player, platform;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            ResetLevel();
        }
    }

    void ResetLevel() {
        player.transform.position = new Vector3(24.62f, 2.4f, 0);
        platform.transform.position = new Vector3(24.62f, 2.4f, 0);
    }
}