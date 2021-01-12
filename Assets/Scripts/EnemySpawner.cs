using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;
    bool hasSpawned;

    private void OnEnable() {
        hasSpawned = false;
    }

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

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, 15f);
    // }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
