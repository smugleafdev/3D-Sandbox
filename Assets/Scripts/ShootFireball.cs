using UnityEngine;

public class ShootFireball : CastBehavior {

    [SerializeField] GameObject fireball;
    [SerializeField] float speed = 100f;

    public override void Cast() {
        // TODO: Improve fireball's fiery effect and turn off the sphere mesh again
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (fireball != null) {
            GameObject instBullet = ObjectUtils.GetOrInstantiate(fireball, transform.position, transform.rotation);
            Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidBody.AddForce(forward * speed);
        }
    }

    private void FixedUpdate() {
        // Debug.DrawLine(transform.position, transform.forward, Color.red, 0.1f);
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 15f, Color.red);
    }
}