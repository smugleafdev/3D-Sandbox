using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    TrailRenderer trail;

    void OnDisable() {
        trail = GetComponent<TrailRenderer>();
        trail.Clear();
    }
}