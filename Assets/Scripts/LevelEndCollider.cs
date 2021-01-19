using UnityEngine;

public class LevelEndCollider : MonoBehaviour {

    LevelController levelController;

    private void Start() {
        levelController = transform.GetComponentInParent<LevelController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            levelController.ResetLevel(other.gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 10f, 25f));
    }
}