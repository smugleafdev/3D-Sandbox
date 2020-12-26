using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private TrailRenderer trail;

    void OnDisable() {
        trail = GetComponent<TrailRenderer>();
        trail.Clear();
    }
}