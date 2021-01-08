using UnityEngine;

public class PlatformCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            transform.GetComponentInParent<PlatformMovement>().active = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            transform.GetComponentInParent<PlatformMovement>().active = false;
        }
    }
}