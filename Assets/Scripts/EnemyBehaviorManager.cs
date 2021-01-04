using System.Collections;
using UnityEngine;

public class EnemyBehaviorManager : MonoBehaviour {

    Transform target;
    [SerializeField] float turnSpeed = 25f;

    EnemyHealthManager enemyHealthManager;
    EnemyAttackManager enemyEmitterManager;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] float attackSpeed = 3f;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealthManager = transform.GetComponent<EnemyHealthManager>();
        enemyEmitterManager = transform.GetComponentInChildren<EnemyAttackManager>();
        StartCoroutine("AttackTargetDelay");
    }

    void Update() {
        if (!enemyHealthManager.isDead) {
            FaceTarget();
        }
    }

    void FaceTarget() {
        // Debug.Log("Facing " + target);
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void AttackTarget() {
        Vector3 targetCenter = new Vector3(target.position.x, target.position.y + 1, target.position.z);
        enemyEmitterManager.transform.LookAt(targetCenter);
        enemyEmitterManager.Attack();
    }

    IEnumerator AttackTargetDelay() {
        yield return new WaitForSeconds(attackSpeed);
        if (!enemyHealthManager.isDead) {
            AttackTarget();
            StartCoroutine("AttackTargetDelay");
        }
    }
}
