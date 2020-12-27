using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour {

    public GameObject[] equippedBullets;
    private GameObject currentBullet;
    public float speed = 100f;
    private bool changed = false;
    private int equippedSlot;

    void Update() {
        int parentSlot = transform.parent.GetComponentInParent<FPSController>().equippedSlot;
        if (parentSlot != equippedSlot) {
            equippedSlot = parentSlot;
            if (equippedSlot == 1) {
                currentBullet = equippedBullets[equippedSlot - 1];
            } else if (equippedSlot == 2) {
                currentBullet = equippedBullets[equippedSlot - 1];
            } else if (equippedSlot == 3) {
                currentBullet = equippedBullets[equippedSlot - 1];
            } else {
                Debug.Log("ruh roh");
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            Fire();
        }
    }

    void Fire() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (currentBullet != null) {
            GameObject instBullet = ObjectUtils.GetOrInstantiate(currentBullet, transform.position, transform.rotation);

            Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidBody.AddForce(forward * speed);
        }
    }
}