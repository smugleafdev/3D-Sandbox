using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyText : MonoBehaviour {

    float despawnTime = 1f;
    float flySpeed = 1f;
    float textIncreaseAmoumt = 1f;
    TextMesh textMesh;

    void Start() {
        textMesh = GetComponent<TextMesh>();
        Destroy(gameObject, despawnTime);
    }

    private void Update() {
        transform.Translate(0, flySpeed * Time.deltaTime, 0);
        textMesh.characterSize += textIncreaseAmoumt * Time.deltaTime;
    }
}