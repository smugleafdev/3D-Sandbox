using UnityEngine;

public static class ObjectUtils {

    static Color damageColor = new Color32(255, 20, 20, 255);
    static Color healColor = new Color32(0, 255, 0, 255);

    public static GameObject GetOrInstantiate(GameObject prefab, Vector3 pos, Quaternion rot) {
        var obj = ObjectPool.GetObjectByTag(prefab.tag) ?? GameObject.Instantiate(prefab);
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        return obj;
    }

    public static void ShowFlyText(GameObject flyTextPrefab, Vector3 position, string effect) {
        float yOffset = 1f;
        float xOffset = Random.Range(-0.5f, 0.5f);
        float zOffset = Random.Range(-0.5f, 0.5f);

        GameObject textObj = GameObject.Instantiate(flyTextPrefab, new Vector3(position.x + xOffset, position.y + yOffset, position.z + zOffset), Quaternion.identity);
        textObj.GetComponent<TextMesh>().text = effect;
        textObj.GetComponent<TextMesh>().color = GetEffectColor(effect);
    }

    static Color GetEffectColor(string effect) {
        if (effect.Contains("-")) {
            return damageColor;
        } else if (effect.Contains("+")) {
            return healColor;
        }

        return Color.white;
    }
}