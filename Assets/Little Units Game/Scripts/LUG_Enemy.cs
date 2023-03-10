using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUG_Enemy : MonoBehaviour
{
    [SerializeField]int health = 10;

    public int Health => health;

    public string someString;

    public delegate void OnEnemyDeathDelegate(LUG_Enemy enemy);
    public static event OnEnemyDeathDelegate onEnemyDeath;

    public void OnTakeDamage(int damage)
    {
        health-= damage;
        if (health <= 0)
            OnDeath();
    }

    private void OnDeath()
    {
        // Make it flashy
        Destroy(gameObject);
        //if(onEnemyDeath != null)
        onEnemyDeath?.Invoke(this);
    }
}
