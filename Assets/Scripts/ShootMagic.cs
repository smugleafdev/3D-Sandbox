using UnityEngine;

public class ShootMagic : CastBehavior {

    public GameObject magicBullet;
    public float speed = 100f;

    // void Start() { }

    // void Update() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         Fire();
    //     }
    // }

    public override void Cast() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        GameObject instBullet = ObjectUtils.GetOrInstantiate(magicBullet, transform.position, transform.rotation);

        Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidBody.AddForce(forward * speed);
    }
}