using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectUtils {
    public static GameObject GetOrInstantiate(GameObject prefab, Vector3 pos, Quaternion rot) {
        var obj = ObjectPool.GetObjectByTag(prefab.tag) ?? GameObject.Instantiate(prefab);
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        return obj;
    }
}