using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    Rigidbody enemyRigidbody;
    [SerializeField] GameObject flyText;
    [SerializeField] int enemyMaxHealth;
    int enemyCurrentHealth;
    bool shouldDie = false;
    [HideInInspector] public bool isDead = false;
    TextMesh hpText;
    Vector3 collisionImpulse;
    ContactPoint contact;

    void Start() {
        enemyCurrentHealth = enemyMaxHealth;
        enemyRigidbody = GetComponent<Rigidbody>();
        hpText = GetComponentInChildren<TextMesh>();
    }

    void Update() {
        if (isDead) {
            HandleDeath();
        }

        hpText.text = enemyCurrentHealth.ToString();
    }

    void FixedUpdate() {
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

    void HandleDeath() {
        Destroy(gameObject, 3f);
        // TODO: Restart spawner stuff probably go here
    }

    void SetMaxHealth() {
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Save this snippet for later
    // void OnGUI() {
    //     GUI.Label(new Rect(10, 10, 100, 20), "Enemy HP: " + enemyCurrentHealth.ToString());
    // }
}