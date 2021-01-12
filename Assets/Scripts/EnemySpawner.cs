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
            Vector3 pos = transform.position;
            // pos.y += 4;
            GameObject enemy = GameObject.Instantiate(enemyPrefab, pos, transform.rotation);
            enemy.transform.SetParent(transform.parent);
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
