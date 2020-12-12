using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    private int enemyCurrentHealth;
    private bool shouldDie = false;

    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            shouldDie = true;
        }

        if (shouldDie)
        {
            HandleDeath();
        }
    }

    public void DamageEnemy(int damage)
    {
        enemyCurrentHealth -= damage;
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
        // TODO: Restart spawner stuff probably go here
    }

    private void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

}