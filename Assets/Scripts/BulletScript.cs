using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    // public float deathTime = 4f;
    private TrailRenderer trail;

    void Start() {
        // Destroy(gameObject, deathTime);
        trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable() {
        trail.Clear();
    }

    void Update() {
    }
}