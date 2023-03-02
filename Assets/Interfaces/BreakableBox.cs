using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour, IDamageable
{
    [SerializeField]
    int maxHealth = 5; 
    int health;

    private void OnValidate()
    {
        if (maxHealth < 1)
            maxHealth = 1;
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void OnTakeDamage(int damageValue, Vector2 dir)
    {
        transform.DOPunchPosition(dir, .5f);
        if (health <= 0)
            OnDestroy();
    }

    private void OnDestroy()
    {
        //Todo: Some effect
        Destroy(gameObject);
    }
}
