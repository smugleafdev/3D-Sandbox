using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool {

    static Dictionary<string, Stack<GameObject>> pools = new Dictionary<string, Stack<GameObject>>();

    public static GameObject GetObjectByTag(string tag) {
        if (!pools.TryGetValue(tag, out var pool) || pool.Count == 0) {
            return null;
        }
        var obj = pool.Pop();
        obj.SetActive(true);
        return obj;
    }

    public static void StashObjectByTag(GameObject obj) {
        obj.SetActive(false);
        if (!pools.TryGetValue(obj.tag, out var pool)) {
            pool = new Stack<GameObject>();
            pools.Add(obj.tag, pool);
        }

        pool.Push(obj);
    }

    public static void Clear() {
        pools.Clear();
    }

}