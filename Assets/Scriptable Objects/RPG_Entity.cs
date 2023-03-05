using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(SpriteRenderer))]
public class RPG_Entity : MonoBehaviour
{
    [SerializeField] RPG_EntityStats stats;

    public int MaxHealth => stats.MaxHealth;
    public int AttackPower => stats.AttackPower;
    public int Defense => stats.Defense;
    public int Speed => stats.Speed;
    public int AttackRange => stats.AttackRange;

    [SerializeField] int health;

    private SpriteRenderer spriteRenderer;

    List<RPG_Enemy> enemies = new List<RPG_Enemy>();

    float attackTimer;
    float attackInterval => (float)(10 - stats.AttackSpeed) * 0.3f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stats.Sprite;
        health = MaxHealth;

        UpdateEnemyList();
        foreach (var enemy in enemies)
        {
            enemy.onDead.AddListener(() => RemoveFromEnemyList(enemy));
        }
    }

    private void UpdateEnemyList()
    { 
        enemies = FindObjectsOfType<RPG_Enemy>().ToList();
    }
    void RemoveFromEnemyList(RPG_Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    private void Update()
    {
        RPG_Enemy targetEnemy = GetClosestEnemy();
        if (targetEnemy == null)
        {
            Debug.Log("Game is Won");
            return;
        }
        LookAtEnemy(targetEnemy);

        if (targetEnemy)
            Follow(targetEnemy.transform.position);
        if (attackTimer >= attackInterval)
        {
            Attack(targetEnemy);
            attackTimer -= attackInterval;
        }
        attackTimer += Time.deltaTime;
    }

    private void LookAtEnemy(RPG_Enemy targetEnemy)
    {
        spriteRenderer.flipX = (targetEnemy.transform.position.x - transform.position.x) > 0;
    }

    private void Attack(RPG_Enemy targetEnemy)
    {
        if(Vector3.Distance(transform.position, targetEnemy.transform.position) < AttackRange)
        {
            targetEnemy.onTakeDamage(AttackPower);
            transform.DOPunchRotation(Vector3.forward * 30, .5f);

        }

    }

    private void Follow(Vector3 targetPosition)
    {
        if(Vector3.Distance(transform.position, targetPosition) > AttackRange - 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }

    private RPG_Enemy GetClosestEnemy()
    {
        float closestDistance = int.MaxValue;
        RPG_Enemy closestEnemy = null;
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}