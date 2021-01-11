using UnityEngine;

public class EnemyAttackManager : MonoBehaviour {

    Transform target;
    Vector3 targetCurrentPos, emitterMuzzlePosition;
    Vector3 targetLastPosition, emitterMuzzleLastPosition;
    Vector3 targetVelocity, emitterVelocity;
    Vector3 targetPredictedFuturePosition;
    float bulletScalar = 10f;
    [SerializeField] GameObject emitter;
    Transform emitterMuzzle;
    CastBehavior castScript;


    void Start() {
        GameObject emitterInstance = GameObject.Instantiate(emitter, transform.position, transform.rotation);
        emitterInstance.transform.SetParent(transform);
        castScript = emitterInstance.GetComponentInChildren<CastBehavior>();
        emitterMuzzle = emitterInstance.transform.Find("EmitterMuzzle").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Attack() {
        if (castScript != null) {
            LookAtTargetPrediction();
            castScript.Cast();
        }
    }

    void LookAtTargetPrediction() {
        Debug.DrawLine(emitterMuzzlePosition, targetPredictedFuturePosition, Color.blue, 1f);
        // transform.LookAt(aimAt);
        emitterMuzzle.LookAt(targetPredictedFuturePosition);
    }

    private void FixedUpdate() {
        CalculateShit();
    }

    void CalculateShit() {
        targetCurrentPos = new Vector3(target.position.x, target.position.y + 1f, target.position.z);
        emitterMuzzlePosition = emitterMuzzle.position;

        emitterVelocity = (emitterMuzzlePosition - emitterMuzzleLastPosition) / Time.fixedDeltaTime;
        targetVelocity = (targetCurrentPos - targetLastPosition) / Time.fixedDeltaTime;

        // Find the relative position and velocities
        Vector3 relativePosition = targetCurrentPos - emitterMuzzlePosition;
        Vector3 relativeVelocity = targetVelocity - emitterVelocity;

        // Calculate the time a bullet will collide
        // if it's possible to hit the target.
        float timeOfCollision = AimAhead(relativePosition, relativeVelocity, bulletScalar);

        // If the time is negative, then we didn't get a solution.
        if (timeOfCollision > 0f) {
            // Aim at the point where the target will be at the time
            // of the collision.
            targetPredictedFuturePosition = targetCurrentPos + timeOfCollision * relativeVelocity;
        }

        targetLastPosition = targetCurrentPos;
        emitterMuzzleLastPosition = emitterMuzzlePosition;
    }

    // TODO: Clamp aim X/Z to platform edges
    // Clamp Y axis - how exactly? I dunno. Maybe to platform bottom vs 7ft? Maybe track player's max height all the time

    float AimAhead(Vector3 delta, Vector3 vr, float muzzleV) {
        // Quadratic equation coefficients a*t^2 + b*t + c = 0
        float a = Vector3.Dot(vr, vr) - muzzleV * muzzleV;
        float b = 2f * Vector3.Dot(vr, delta);
        float c = Vector3.Dot(delta, delta);

        float det = b * b - 4f * a * c;

        // If the determinant is negative, then there is no solution
        if (det > 0f) {
            return 2f * c / (Mathf.Sqrt(det) - b);
        } else {
            return -1f;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(emitterMuzzlePosition, 0.05f);
        Gizmos.DrawWireCube(targetPredictedFuturePosition, new Vector3(0.1f, 0.1f, 0.1f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(targetCurrentPos, 0.05f);
        // Gizmos.DrawWireCube(target.position, new Vector3(0.1f, 0.1f, 0.1f));
    }

    // float offset = 12f;

    // TODO: Need to make the bullet speed either known, or set from higher in the chain than the Shoot.cs
    // void OnGUI() {
    //     GUI.Label(new Rect(10, offset * 4, 100, 20), string.Format("Distance: {0}", distance));
    //     GUI.Label(new Rect(10, offset * 5, 1000, 20), string.Format("My vel: {0}", myVel));
    //     GUI.Label(new Rect(10, offset * 6, 1000, 20), string.Format("Target vel: {0}", targVel));
    // }
}