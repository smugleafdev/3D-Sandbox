using UnityEngine;

public class EnemyBehaviorManager : MonoBehaviour {

    Transform target;
    [SerializeField] float turnSpeed = 25f;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        FaceTarget();
    }

    private void FaceTarget() {
        // Debug.Log("Facing " + target);
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
