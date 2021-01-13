using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;
    bool hasSpawned;

    private void OnEnable() {
        hasSpawned = false;
    }

    // TODO: Bug here. If player is within the collision sphere when teleported (by level reset)
    // it triggers instantly, even though it should still be inactive (player position reset first, then spawners set)
    private void OnTriggerEnter(Collider other) {
        if (!hasSpawned && other.tag == "Player") {
            hasSpawned = true;
            SpawnEnemy();
            gameObject.SetActive(false);
        }
    }

    void SpawnEnemy() {
        if (enemyPrefab != null) {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out hit)) {
                GameObject enemy = GameObject.Instantiate(enemyPrefab, hit.point, transform.rotation);
                enemy.transform.SetParent(transform.parent);
                Debug.Log("Spawning");
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z));
    }
}