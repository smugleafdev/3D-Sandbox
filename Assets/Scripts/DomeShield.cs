using System.Collections.Generic;
using UnityEngine;

public class DomeShield : MonoBehaviour {

    GameObject paneParent;
    Pane[] panes;
    // [SerializeField] float shieldGrowthTime = 2f;
    float timer, debugTimerGrow, debugTimerShrink;
    float currentSize;
    float finalSize = 1.5f;
    bool isDying;

    float journeyTime = 10f;
    float slerpTime;
    bool isSlerping;

    Vector3 targetPosition;

    int blockedDamage;

    void Start() {
        timer = 0f;
        debugTimerGrow = 0f;
        debugTimerShrink = 0f;
        currentSize = 0f;
        isDying = false;

        slerpTime = 0f;
        isSlerping = false;

        targetPosition = Vector3.zero;

        blockedDamage = 0;

        paneParent = GameObject.FindGameObjectWithTag("Plane");
        panes = paneParent.transform.GetComponentsInChildren<Pane>();
    }

    void Update() {
        timer += Time.deltaTime;

        if (!isDying && currentSize < finalSize) {
            currentSize += Time.deltaTime;
            debugTimerGrow += Time.deltaTime;
        } else if (isDying && currentSize > 0 && !isSlerping) {
            currentSize -= Time.deltaTime;
            debugTimerShrink += Time.deltaTime;
        }

        if (timer >= 0) {
            isDying = true;
        }

        // Option B: Slerp shield
        if (isSlerping) {
            // TODO: If slerping functionally (and not just visually), update calculation
            // to multiply fracComplete by arc length. Otherwise, a short slerp takes
            // as long as a long slerp
            float fracComplete = (Time.time - slerpTime) / journeyTime;
            transform.position = Vector3.Slerp(transform.position, targetPosition, fracComplete);

            if (transform.position == targetPosition) {
                isSlerping = false;
            }
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, currentSize / 2, LayerMask.GetMask("TankUI"));
        List<GameObject> hitList = new List<GameObject>();
        foreach (Collider hit in hitColliders) {
            hitList.Add(hit.transform.parent.gameObject);
        }

        // paneParent.GetComponent<Dome>().hitList = hitList;

        foreach (Pane pane in panes) {
            pane.ToggleActivation(hitList.Contains(pane.gameObject));
        }

        // do overlap sphere
        // get colliding panels
        // collidingPanels = overlapColliders.Select(coll => coll.gameObject.GetComponentInParent<Pane>())

        // foreach panel {
        //  panel.setActive(collidingPanels.Contains(panel))
        //}

        if (currentSize <= 0) {
            Destroy(gameObject);
        }
    }

    public void SetShieldPosition(Vector3 position) {
        if (currentSize == 0) {
            transform.position = position;
        } else {
            // Thoughts: Maybe keep this code purely for graphical polish, and teleport the shield functionally

            // Option A: Teleport shield (no slerp)
            // transform.position = Vector3.MoveTowards(transform.position, position, 0.1f);

            // Option B: Slerp shield
            // if (targetPosition != position) {
            if (!isSlerping) {
                slerpTime = Time.time;
            }
            isSlerping = true;
            targetPosition = position;
        }

        timer = 0;
        isDying = false;
    }

    // public void BlockHit(int damage) {
    //     blockedDamage = damage;
    // }

    // private void OnTriggerEnter(Collider collision) {
    // void OnCollisionEnter(Collision collision) {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("TankUI")) {
    //         collision.gameObject.GetComponentInParent<Pane>().ActivateExterior();
    //     }
    // }

    // // private void OnTriggerExit(Collider collision) {
    // private void OnCollisionExit(Collision collision) {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("TankUI")) {
    //         collision.gameObject.GetComponentInParent<Pane>().DeactivateExterior();
    //     }
    // }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.cyan;
    //     Gizmos.DrawWireSphere(transform.position, currentSize / 2);
    // }

    float offset = 12f;
    private void OnGUI() {
        GUI.Label(new Rect(10, offset * 2, 200, 20), $"Grow time: {debugTimerGrow}");
        GUI.Label(new Rect(10, offset * 3, 200, 20), $"Shrink time: {debugTimerShrink}");
        GUI.Label(new Rect(10, offset * 4, 200, 20), $"Blocked damage: {blockedDamage}");
    }
}