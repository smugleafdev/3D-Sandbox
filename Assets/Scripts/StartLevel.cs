using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class ObjectPoolPair {
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public int prefabAmount;
}

public class StartLevel : MonoBehaviour {
    public ObjectPoolPair[] pairs;

    void Start() {
        pairs.ToList().ForEach(pair => {
            // Debug.Log($"{pair.prefab.tag} - {pair.prefabAmount}");
            Enumerable.Range(0, pair.prefabAmount).ToList().ForEach(_ => {
                ObjectPool.StashObjectByTag(GameObject.Instantiate(pair.prefab));
            });
        });
    }
}