using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {
    public GameObject bullet;
    public float speed = 100f;

    void Start() { }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Fire();
        }
    }

    void Fire() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // GameObject instBullet = Instantiate(bullet, transform.position, transform.rotation);

        // GameObject instBullet = ObjectPooler.SharedInstance.GetPooledObject();
        // if (instBullet != null) {
        //     bullet.transform.position = transform.position;
        //     bullet.transform.rotation = transform.rotation;
        //     bullet.SetActive(true);
        // }

        GameObject instBullet = ObjectPooler.SharedInstance.GetPooledObject(transform.position, transform.rotation);
        Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidBody.AddForce(forward * speed);
    }
}