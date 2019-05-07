using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        // TODO
        Debug.Log(gameObject.name + " died");
    }

    public int GetHealth()
    {
        return health;
    }
}
