using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField] GameObject platform;
    GameObject player;
    List<GameObject> spawners = new List<GameObject>();

    private void Start() {
        spawners = FindChildObjectsWithTag("EnemySpawner");
    }

    public void ResetLevel(GameObject playerCollided) {
        player = playerCollided;
        ResetPlatformAndPlayer();
        DespawnEnemies();
        ResetSpawners();
    }

    void ResetPlatformAndPlayer() {
        player.transform.position = new Vector3(24.62f, 2.4f, 0);
        platform.transform.position = new Vector3(24.62f, 2.4f, 0);
    }

    private void DespawnEnemies() {
        List<GameObject> enemiesToDespawn = FindChildObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemiesToDespawn) {
            Destroy(enemy);
        }
    }

    private void ResetSpawners() {
        foreach (GameObject spawner in spawners) {
            spawner.SetActive(true);
        }
    }

    List<GameObject> FindChildObjectsWithTag(string tag) {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (child.tag == tag) {
                children.Add(child.gameObject);
            }
        }
        return children;
    }
}