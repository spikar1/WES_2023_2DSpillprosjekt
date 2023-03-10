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

    public bool IsDead => health <= 0;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameEvent onDeathEvent;

    Tween tween;

    public static event Action<RPG_Enemy> OnEnemyDeath;

    public delegate void OnFoo(int someInt, float someFloat);
    public static event OnFoo onFoo;

    private void Awake()
    {
        health = stats.MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stats.Sprite;

    }
    private void OnEnable() => onFoo += Boo;
    private void OnDisable() => onFoo -= Boo;

    void Boo(int i, float f) => Debug.Log($"i : {i}, f: {f}");

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
        this.enabled = false;
        onDeathEvent.Raise();
        OnEnemyDeath(this);
    }
}
