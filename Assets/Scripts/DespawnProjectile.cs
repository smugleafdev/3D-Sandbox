using System.Collections;
using UnityEngine;

public class DespawnProjectile : MonoBehaviour {

    [SerializeField] float deathTime = 4f;
    [SerializeField] int damage = 0;

    void OnEnable() {
        StartCoroutine("DespawnAfterTime");
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponentInParent<EnemyHealthManager>().DamageEnemy(damage, collision);
        } else if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponentInParent<FPSController>().DamagePlayer(damage);
        }
        Deactivate();
    }

    void Deactivate() {
        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.velocity = Vector3.zero;
        ObjectPool.StashObjectByTag(gameObject);
    }

    IEnumerator DespawnAfterTime() {
        yield return new WaitForSeconds(deathTime);
        Deactivate();
    }
}