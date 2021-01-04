using UnityEngine;

public class ShootMagic : CastBehavior {

    [SerializeField] GameObject magicBullet;
    [SerializeField] float speed = 100f;

    public override void Cast() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        GameObject instBullet = ObjectUtils.GetOrInstantiate(magicBullet, transform.position, transform.rotation);

        Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
        instBulletRigidBody.AddForce(forward * speed);
    }
}