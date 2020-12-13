using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnProjectile : MonoBehaviour
{
    public float deathTime = 4f;

    void Start()
    {
        Destroy(gameObject, deathTime);
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // collision.gameObject.GetComponentInParent<EnemyHealthManager>().DamageEnemy(5);
            collision.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(5);
        }
        Destroy(gameObject);
    }
}