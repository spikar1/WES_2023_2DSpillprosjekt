using System;
using UnityEngine;

public class LUG_Entity : MonoBehaviour
{
    //A reference to a Stat asset. This needs to be created and referenced for the code to work
    [SerializeField] LUG_EntityStats stats;

    private SpriteRenderer sr;
    private float cooldown;
    private float attackInterval = 1; //assosiert med speed stat

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        LUG_Enemy.onEnemyDeath += OnEnemyDeath;
    }
    //To avoid memory leaks, we need to unsubscribe from the events aswell
    private void OnDisable()
    {
        LUG_Enemy.onEnemyDeath -= OnEnemyDeath;
    }

    void OnEnemyDeath(LUG_Enemy enemy)
    {
        //response to the on death event defined in LUG_Enemy script
        Debug.Log($"{enemy} has died");
        enemy.someString = "Haha, you died";
    }

    private void Update()
    {

        // Sjekke etter nærmeste fiende
        
        LUG_Enemy targetEnemy = FindClosestEnemy();
        if(targetEnemy == null) 
        {
            Debug.Log("No enemies found in scene");
            return;
        }
        // Sjå på fiende
        // Følge etter fiende
        FollowEnemy(targetEnemy);
        // Angripe i intervaller
        if(cooldown <= 0)
        {
            if (CanAttackEnemy(targetEnemy))
                AttackEnemy(targetEnemy);
            cooldown = attackInterval;
        }
        cooldown -= Time.deltaTime;
    }

    private void AttackEnemy(LUG_Enemy enemy)
    {
        enemy.OnTakeDamage(stats.AttackPower);
    }

    private bool CanAttackEnemy(LUG_Enemy enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position) < stats.AttackRange; //todo: bind stats
    }

    private void FollowEnemy(LUG_Enemy targetEnemy)
    {
        float distance = Vector3.Distance(transform.position, targetEnemy.transform.position);
        if(distance >= stats.AttackRange - 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, stats.Speed * Time.deltaTime);
    }

    private LUG_Enemy FindClosestEnemy()
    {
        //Find all enemies in scene
        LUG_Enemy[] enemies = FindObjectsOfType<LUG_Enemy>();
        //Create variable for storing closest enemy
        LUG_Enemy closestEnemy = null;
        //Set Min distance to a stupidly high number
        float minDistance = float.MaxValue;

        //Foreach enemy in scene, update closest
        foreach (var enemy in enemies)
        {

            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < minDistance) 
            {
                minDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
