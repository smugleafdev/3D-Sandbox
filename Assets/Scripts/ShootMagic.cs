using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMagic : MonoBehaviour
{
    public GameObject magicBullet;
    // public float speed = 100f;

    void Start() { }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        GameObject instBullet = Instantiate(magicBullet, transform.position, transform.rotation);
        Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
        // instBulletRigidBody.AddForce(forward * speed);
    }
}