using UnityEngine;

public class EnemyAttackManager : MonoBehaviour {

    [SerializeField] GameObject emitter;
    private CastBehavior castScript;

    private void Start() {
        GameObject emitterInstance = GameObject.Instantiate(emitter, transform.position, transform.rotation);
        emitterInstance.transform.parent = transform;
        castScript = emitterInstance.GetComponent<CastBehavior>();
    }

    public void Attack() {
        if (castScript != null) {
            castScript.Cast();
        }
    }
}