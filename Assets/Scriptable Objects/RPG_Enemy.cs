using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class RPG_Enemy : MonoBehaviour
{
    [SerializeField] RPG_EntityStats stats;

    [SerializeField] Gradient healthGradient = new Gradient();
    float HealthFactor => (float)health / stats.MaxHealth;
    int health;

    SpriteRenderer spriteRenderer;

    public UnityEvent onDead;

    Tween tween;

    private void Start()
    {
        health = stats.MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stats.Sprite;
    }

    public void onTakeDamage(int damage)
    {
        health -= damage;
        spriteRenderer.color = Color.red;

        if(tween != null)
            tween.Kill();

        tween = spriteRenderer.DOColor(healthGradient.Evaluate(HealthFactor), .5f).SetEase(Ease.InCubic);

        if (health <= 0)
            OnDead();
    }

    private void OnDead()
    {
        Destroy(gameObject);
        onDead.Invoke();
    }
}
