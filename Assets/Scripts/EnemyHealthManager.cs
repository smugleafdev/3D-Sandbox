using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {
    public int enemyMaxHealth;
    private int enemyCurrentHealth;
    private bool shouldDie = false;

    public Text hp;

    void Start() {
        enemyCurrentHealth = enemyMaxHealth;
        // hpText = GameObject.FindWithTag("HPText");
        hp = GetComponent<Text>();
        hp.text = 36.ToString();
    }

    void Update() {
        if (enemyCurrentHealth <= 0) {
            shouldDie = true;
        }

        if (shouldDie) {
            HandleDeath();
        }

        hp.text = enemyCurrentHealth.ToString();
    }

    public void DamageEnemy(int damage) {
        enemyCurrentHealth -= damage;
    }

    private void HandleDeath() {
        Destroy(gameObject);
        // TODO: Restart spawner stuff probably go here
    }

    private void SetMaxHealth() {
        enemyCurrentHealth = enemyMaxHealth;
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 100, 20), "Enemy HP: " + enemyCurrentHealth.ToString());
    }

}