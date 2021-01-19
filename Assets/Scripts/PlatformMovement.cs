using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    GameObject player;
    [SerializeField] float speed = 2f;
    public bool active;

    private void FixedUpdate() {
        if (active && player != null) {
            Vector3 move = new Vector3(speed * Time.fixedDeltaTime, 0, 0);
            transform.Translate(move);
            player.transform.Translate(move, transform);
        }
    }

    public void PlayerActivated(GameObject playerOnPlatform) {
        player = playerOnPlatform;
        active = true;
    }

    public void PlayerDeactivated() {
        player = null;
        active = false;
    }
}