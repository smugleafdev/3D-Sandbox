using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public GameObject flyText;
    public int enemyMaxHealth;
    private int enemyCurrentHealth;
    private bool shouldDie = false;

    public GameObject hpObj;
    TextMesh hpText;

    void Start() {
        enemyCurrentHealth = enemyMaxHealth;
        hpText = GetComponentInChildren<TextMesh>();
    }

    void Update() {
        if (enemyCurrentHealth <= 0) {
            shouldDie = true;
        }

        if (shouldDie) {
            HandleDeath();
        }

        hpText.text = enemyCurrentHealth.ToString();
    }

    public void DamageEnemy(int damage) {
        enemyCurrentHealth -= damage;
        ObjectUtils.ShowFlyText(flyText, transform.position, $"-{damage}");
    }

    private void HandleDeath() {
        Destroy(gameObject);
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