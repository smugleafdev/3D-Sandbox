using UnityEngine;

public class ShootBullet : MonoBehaviour {

    [SerializeField] GameObject bullet;
    [SerializeField] float speed = 100f;
    int equippedSlot;

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            Fire();
        }
    }

    void Fire() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (bullet != null) {
            GameObject instBullet = ObjectUtils.GetOrInstantiate(bullet, transform.position, transform.rotation);
            Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidBody.AddForce(forward * speed);
        }
    }
}