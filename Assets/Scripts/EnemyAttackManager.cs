using UnityEngine;

public class EnemyAttackManager : MonoBehaviour {

    [SerializeField] GameObject emitter;
    CastBehavior castScript;

    void Start() {
        GameObject emitterInstance = GameObject.Instantiate(emitter, transform.position, transform.rotation);
        emitterInstance.transform.parent = transform;
        castScript = emitterInstance.GetComponentInChildren<CastBehavior>();
    }

    public void Attack() {
        if (castScript != null) {
            castScript.Cast();
        }
    }
}