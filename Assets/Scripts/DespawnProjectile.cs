using System.Collections;
using UnityEngine;

public class DespawnProjectile : MonoBehaviour {

    [SerializeField] float deathTime = 4f;
    [SerializeField] int damage = 0;

    void OnEnable() {
        StartCoroutine("DespawnAfterTime");
    }

    void OnCollisionEnter(Collision collision) {
        string tag = collision.gameObject.tag;
        if (tag == "Enemy") {
            collision.gameObject.GetComponentInParent<EnemyHealthManager>().DamageEnemy(damage, collision);
        } else if (tag == "Player") {
            collision.gameObject.GetComponentInParent<FPSController>().DamagePlayer(damage);
            ObjectUtils.AddShotHit();
        } else if (tag == "BulletEnemy" && transform.gameObject.tag == "BulletEnemy") {
            ObjectUtils.SubtractBulletCollision();
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