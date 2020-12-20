using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public bool growOverAmount = true;

    void Awake() {
        SharedInstance = this;
    }

    void Start() {
        pooledObjects = new List<GameObject>();
        GameObject obj;
        for (int i = 0; i < amountToPool; i++) {
            obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // public GameObject GetPooledObject() {
    //     for (int i = 0; i < pooledObjects.Count; i++) {
    //         if (!pooledObjects[i].activeInHierarchy) {
    //             return pooledObjects[i];
    //         }
    //     }
    //     return null;
    // }

    public GameObject GetPooledObject(Vector3 position, Quaternion rotation) {
        foreach (GameObject item in pooledObjects) {
            if (!item.activeInHierarchy) {
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.SetActive(true);
                return item;
            }
        }

        if (growOverAmount) {
            GameObject instance = Instantiate(objectToPool, position, rotation);
            pooledObjects.Add(instance);
            return instance;
        }

        return null;
    }

}