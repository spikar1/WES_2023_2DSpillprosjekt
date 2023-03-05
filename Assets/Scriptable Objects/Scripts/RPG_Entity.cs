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

    // We use expression bodied member to get variables from the stat sheet.
    // This is equivalent with writing MaxHealth { get { return stats.MaxHealth; } }
    // We mostly prefer readable code however
    public int MaxHealth => stats.MaxHealth;
    public int AttackPower => stats.AttackPower;
    public int Defense => stats.Defense;
    public int Speed => stats.Speed;
    public int AttackRange => stats.AttackRange;

    [SerializeField] int health;

    private SpriteRenderer spriteRenderer;

    [SerializeField]List<RPG_Enemy> enemies = new List<RPG_Enemy>();

    float attackTimer;
    float attackInterval => (float)(10 - stats.AttackSpeed) * 0.3f;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stats.Sprite;
        health = MaxHealth;

        UpdateEnemyList();

        RPG_Enemy.OnEnemyDeath += RemoveEnemyFromList;
    }

    private void RemoveEnemyFromList(RPG_Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public void UpdateEnemyList()
    {
        enemies.Clear();
        foreach (var enemy in FindObjectsOfType<RPG_Enemy>())
        {
            if (!enemy.IsDead)
                enemies.Add(enemy);
        }
    }


    private void Update()
    {
        //Check if there is any enemies to attack
        RPG_Enemy targetEnemy = GetClosestEnemy();
        if (targetEnemy == null)
        {
            Debug.Log("Game is Won");
            return;
        }
        //Always face enemy attacked
        LookAtEnemy(targetEnemy);

        //Walk towards enemy to be attacked
        Follow(targetEnemy.transform.position);

        //Using a cooldown we attack at set intervals (based on the speed stat)
        if (attackTimer >= attackInterval)
        {
            Attack(targetEnemy);
            attackTimer -= attackInterval;
        }

        //Count upwards for attack cooldown
        attackTimer += Time.deltaTime;
    }

    private void LookAtEnemy(RPG_Enemy targetEnemy)
    {
        // If enemy is positioned more to the right than this unit, flip the sprite
        spriteRenderer.flipX = targetEnemy.transform.position.x > transform.position.x;
    }

    private void Attack(RPG_Enemy targetEnemy)
    {
        //Using a range stat we attack when in proximity
        if(Vector3.Distance(transform.position, targetEnemy.transform.position) < AttackRange)
        {
            targetEnemy.onTakeDamage(AttackPower);
            transform.DOPunchRotation(Vector3.forward * 30, .5f);

        }

    }

    private void Follow(Vector3 targetPosition)
    {
        //Stop chasing is we're close enought to attack (We add som leeway here, 0.1f)
        if(Vector3.Distance(transform.position, targetPosition) > AttackRange - 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
    }

    private RPG_Enemy GetClosestEnemy()
    {
        //Iterate through our list of enemies in the scene and find the closest
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