using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyText : MonoBehaviour {

    float despawnTime = 1f;
    float flySpeed = 0.01f;
    float textIncreaseAmoumt = 0.01f;
    TextMesh textMesh;

    void Start() {
        textMesh = GetComponent<TextMesh>();
        Destroy(gameObject, despawnTime);
    }

    private void Update() {
        transform.Translate(0, flySpeed, 0);
        textMesh.characterSize += textIncreaseAmoumt;
    }
}