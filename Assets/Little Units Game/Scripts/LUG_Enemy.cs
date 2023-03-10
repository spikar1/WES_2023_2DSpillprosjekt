using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUG_Enemy : MonoBehaviour
{
    [SerializeField]int health = 10;

    public int Health => health;

    public string someString;

    //We define the signature of our delegate events first, much like we have to do when defining enums
    public delegate void OnEnemyDeathDelegate(LUG_Enemy enemy);
    //We create a delegate event from the signature described over. This is what other entities in the game can subscribe to
    public static event OnEnemyDeathDelegate onEnemyDeath;

    public void OnTakeDamage(int damage)
    {
        health-= damage;
        if (health <= 0)
            OnDeath();
    }

    private void OnDeath()
    {
        //Optimally you would not destroy the gameObject, but rather have it leave some permanence (body)
        //but the enemy should be somehow marked as dead
        Destroy(gameObject);
        // The ? is used to null check before invoking, to avoid errors
        //We pass "this" to the event to let other entities know what enemy died. 
        //(This refers to the object this code is executed on)
        onEnemyDeath?.Invoke(this);
    }
}
