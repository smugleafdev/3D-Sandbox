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
        ShowFlyText($"- {damage}", Color.red);
    }

    private void ShowFlyText(string effect, Color color) {
        if (flyText != null) {
            float yOffset = 1f;

            GameObject textObj = Instantiate(flyText, new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), Quaternion.identity);
            textObj.GetComponent<TextMesh>().text = effect;
            textObj.GetComponent<TextMesh>().color = color;
        }
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