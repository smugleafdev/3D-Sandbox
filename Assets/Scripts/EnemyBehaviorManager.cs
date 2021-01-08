using System.Collections;
using UnityEngine;

public class EnemyBehaviorManager : MonoBehaviour {

    Transform target;
    EnemyHealthManager enemyHealthManager;
    EnemyAttackManager enemyEmitterManager;
    [SerializeField] float turnSpeed = 3f;
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] float attackSpeed = 3f;
    [SerializeField] float lookRange = 10f; // 
    [SerializeField] float attackRange = 5f; // Might want 50f eventually
    bool canAttack, targetPerceived, targetInVisibleRange, targetInAttackRange;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealthManager = transform.GetComponent<EnemyHealthManager>();
        enemyEmitterManager = transform.GetComponentInChildren<EnemyAttackManager>();
        StartCoroutine("AttackTargetDelay");
    }

    void Update() {
        CalculateRanges();
        FaceTarget();
        AttackTarget();
    }

    void FaceTarget() {
        if (!enemyHealthManager.isDead && targetInVisibleRange) {
            // Debug.Log("Facing " + target);
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
    }

    void AttackTarget() {
        if (canAttack && targetInAttackRange && targetPerceived) {
            Vector3 targetCenter = new Vector3(target.position.x, target.position.y + 1, target.position.z);
            enemyEmitterManager.transform.LookAt(targetCenter);
            enemyEmitterManager.Attack();
            StartCoroutine("AttackTargetDelay");
            canAttack = false;
        }
    }

    IEnumerator AttackTargetDelay() {
        yield return new WaitForSeconds(attackSpeed);
        if (!enemyHealthManager.isDead) {
            canAttack = true;
        }
    }

    void CalculateRanges() {
        int playerMask = 1 << LayerMask.NameToLayer("PlayerLayer");
        // TODO: Replace below (million years from now) with raycast to platform center, rather than player ("target")
        float distance = Vector3.Distance(target.position, transform.position);
        targetInVisibleRange = distance < lookRange;
        targetInAttackRange = distance < attackRange;
        targetPerceived = Physics.Raycast(transform.position, (target.position - transform.position), lookRange);
        // WIP don't look //Physics.RaycastAll(transform.position, (target.position - transform.position), lookRange, playerMask).Length > 0;
        // TODO: targetPerceived = nothing in the way of a shot allowed except other players
    }
}