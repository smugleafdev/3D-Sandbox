using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    Rigidbody enemyRigidbody;
    public GameObject flyText;
    public int enemyMaxHealth;
    private int enemyCurrentHealth;
    private bool shouldDie = false;
    [HideInInspector] public bool isDead = false;
    private TextMesh hpText;
    private Vector3 collisionImpulse;
    private ContactPoint contact;

    private void Start() {
        enemyCurrentHealth = enemyMaxHealth;
        enemyRigidbody = GetComponent<Rigidbody>();
        hpText = GetComponentInChildren<TextMesh>();
    }

    private void Update() {
        if (isDead) {
            HandleDeath();
        }

        hpText.text = enemyCurrentHealth.ToString();
    }

    private void FixedUpdate() {
        if (collisionImpulse != Vector3.zero) {
            // TODO: This force isn't quite right, but it's close.
            // Running the collision naturally, it's weaker than this hit.
            // May not be an issue.
            enemyRigidbody.AddForceAtPosition(-collisionImpulse / 2, contact.point, ForceMode.Impulse);
            collisionImpulse = Vector3.zero;
        }
    }

    public void DamageEnemy(int damage, Collision collision) {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0) {
            isDead = true;
            enemyRigidbody.isKinematic = false;
            GetComponent<EnemyBehaviorManager>().enabled = false;
            collisionImpulse = collision.impulse;
            contact = collision.GetContact(0);
        }
        ObjectUtils.ShowFlyText(flyText, transform.position, $"-{damage}");
    }

    private void HandleDeath() {
        Destroy(gameObject, 3f);
        // TODO: Restart spawner stuff probably go here
    }

    private void SetMaxHealth() {
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Save this snippet for later
    // void OnGUI() {
    //     GUI.Label(new Rect(10, 10, 100, 20), "Enemy HP: " + enemyCurrentHealth.ToString());
    // }
}