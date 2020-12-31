using System.Collections;
using UnityEngine;

public class DespawnProjectile : MonoBehaviour {

    public float deathTime = 4f;

    private void OnEnable() {
        StartCoroutine("DespawnAfterTime");
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponentInParent<EnemyHealthManager>().DamageEnemy(5);
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