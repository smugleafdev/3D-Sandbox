using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private TrailRenderer trail;

    void Start() {
        trail = GetComponent<TrailRenderer>();
    }

    void OnEnable() {
        trail.Clear();
    }

    void Update() {
    }
}