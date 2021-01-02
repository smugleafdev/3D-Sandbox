using UnityEngine;

public class ShootFireball : CastBehavior {

    public GameObject fireball;
    public float speed = 100f;

    public override void Cast() {
        // TODO: Improve fireball's fiery effect and turn off the sphere mesh again
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (fireball != null) {
            GameObject instBullet = ObjectUtils.GetOrInstantiate(fireball, transform.position, transform.rotation);

            Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidBody.AddForce(forward * speed);
        }
    }
}