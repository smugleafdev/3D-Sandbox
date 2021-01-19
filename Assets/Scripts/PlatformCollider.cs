using UnityEngine;

public class PlatformCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            transform.GetComponentInParent<PlatformMovement>().PlayerActivated(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            transform.GetComponentInParent<PlatformMovement>().PlayerDeactivated();
        }
    }
}