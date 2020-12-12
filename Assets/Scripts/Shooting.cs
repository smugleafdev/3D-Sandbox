using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float speed = 100f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);

            GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidBody.AddForce(forward * speed);
        }
    }
}
