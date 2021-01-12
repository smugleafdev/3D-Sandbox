using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    List<GameObject> spawners = new List<GameObject>();

    private void Start() {
        spawners = FindChildObjectsWithTag("EnemySpawner");
    }

    public void ResetLevel() {
        DespawnEnemies();
        ResetSpawners();
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