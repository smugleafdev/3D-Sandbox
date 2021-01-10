using UnityEngine;

public class EnemyAttackManager : MonoBehaviour {

    [SerializeField] GameObject playerObj;
    Transform target;
    Vector3 targetLastPos, myLastPos;
    float targetVelocity, myVelocity;
    Vector3 targVel, myVel;
    float bulletScalar = 10f;
    Vector3 targetCurrentPos;
    [SerializeField] GameObject emitter;
    CastBehavior castScript;

    float distance;
    float offset = 12f;

    Vector3 aimAt;

    void Start() {
        GameObject emitterInstance = GameObject.Instantiate(emitter, transform.position, transform.rotation);
        emitterInstance.transform.SetParent(transform);
        castScript = emitterInstance.GetComponentInChildren<CastBehavior>();
        target = playerObj != null && playerObj.transform != null ? playerObj.transform : GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Attack() {
        if (castScript != null) {
            AimAt();
            castScript.Cast();
        }
    }

    void AimAt() {
        // aimAt = FirstOrderIntercept(transform.position, myVel, bulletScalar, targetCurrentPos, targVel);
        // Debug.DrawLine(transform.position, target.position, Color.red, 2f);

        // transform.LookAt(aimAt);
        // Quaternion lookRotation = Quaternion.LookRotation(transform.position + target.GetComponent<Rigidbody>().velocity);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
        // float bulletScalar = 10f;

        float distance = Vector3.Distance(transform.position, target.position);//distance in between in meters
        float travelTime = distance / bulletScalar;//time in seconds the shot would need to arrive at the target
        Vector3 targetCenter = new Vector3(target.position.x, target.position.y + 1f, target.position.z);
        Vector3 aimAt = targetCenter + targVel * travelTime;
        Debug.DrawLine(transform.position, aimAt, Color.blue);
        transform.LookAt(aimAt);
    }

    private void Update() {
        transform.rotation = transform.parent.rotation;
    }

    private void FixedUpdate() {
        Vector3 targetInitPosition = target.position;
        targetCurrentPos = target.position;
        float targetDistance = Vector3.Distance(targetCurrentPos, targetLastPos);
        float targetVelocity = targetDistance / Time.fixedDeltaTime;

        myVelocity = Vector3.Distance(transform.position, myLastPos) / Time.fixedDeltaTime;


        distance = Vector3.Distance(transform.position, targetCurrentPos);

        myVel = (transform.position - myLastPos) / Time.fixedDeltaTime;
        targVel = (target.position - targetLastPos) / Time.fixedDeltaTime;

        if (distance < 15f) AimAt();

        targetLastPos = target.position;
        myLastPos = transform.position;


        // transform.LookAt(aimAt);
        // transform.LookAt(aimAt, Vector3.up);
        // transform.
        Debug.DrawRay(transform.position, transform.forward * 15f, Color.green);

        // Debug.DrawLine(transform.position, transform.forward, color);
    }

    // TODO: Need to make the bullet speed either known, or set from higher in the chain than the Shoot.cs
    void OnGUI() {
        GUI.Label(new Rect(10, offset * 4, 100, 20), string.Format("Distance: {0}", distance));
        GUI.Label(new Rect(10, offset * 5, 1000, 20), string.Format("My vel: {0}", myVel));
        GUI.Label(new Rect(10, offset * 6, 1000, 20), string.Format("Target vel: {0}", targVel));
    }

    //first-order intercept using absolute target position
    public static Vector3 FirstOrderIntercept
    (
        Vector3 shooterPosition,
        Vector3 shooterVelocity,
        float shotSpeed,
        Vector3 targetPosition,
        Vector3 targetVelocity
    ) {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    ) {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f) {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f) { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f) {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            } else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        } else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }

}