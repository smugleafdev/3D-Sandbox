using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMagic : MonoBehaviour {
    public GameObject magicBullet;
    // public float speed = 100f;

    void Start() { }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Fire();
        }
    }

    void Fire() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        // GameObject instBullet = Instantiate(magicBullet, transform.position, transform.rotation);
        // GameObject instBullet = ObjectPooler.SharedInstance.GetPooledObject();
        GameObject instBullet = ObjectPooler.SharedInstance.GetPooledObject(transform.position, transform.rotation);

        // if (instBullet != null) {
        //     magicBullet.transform.position = transform.position;
        //     magicBullet.transform.rotation = transform.rotation;
        //     magicBullet.SetActive(true);
        // }
        //Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
        // instBulletRigidBody.AddForce(forward * speed);
    }
}