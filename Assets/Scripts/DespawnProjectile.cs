using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnProjectile : MonoBehaviour {

    public float deathTime = 4f;
    public float timer = 0f;

    void Start() {
        // Destroy(gameObject, deathTime);
        timer = 0f;

    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= deathTime) {
            // timer = 0f;
            // gameObject.SetActive(false);
            Deactivate();
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            // collision.gameObject.GetComponentInParent<EnemyHealthManager>().DamageEnemy(5);
            collision.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(5);
        }
        // Destroy(gameObject);
        // gameObject.SetActive(false);
        Deactivate();
    }

    void Deactivate() {
        timer = 0f;
        gameObject.SetActive(false);
        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.velocity = Vector3.zero;
    }
}