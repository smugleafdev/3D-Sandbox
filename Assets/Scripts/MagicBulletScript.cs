using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletScript : MonoBehaviour {

    [SerializeField] float speed = 10f;
    TrailRenderer trail;

    void OnDisable() {
        trail = GetComponent<TrailRenderer>();
        trail.Clear();
    }

    void Update() {
        // transform.Translate(transform.forward * speed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}