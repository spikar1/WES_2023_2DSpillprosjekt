using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Stats", menuName = "RPG Game/Create new Entity Stats")]
public class RPG_EntityStats : ScriptableObject
{
    [SerializeField] private string entityName;
    [SerializeField] private int maxHealth;
    [SerializeField] private int attack; 
    [SerializeField] private int defense;
    [SerializeField] private int speed;
    [SerializeField] private int attackSpeed;
    [SerializeField] private int attackRange;

    [SerializeField] private Sprite sprite;

    public string EntityName => entityName;
    public int MaxHealth => maxHealth;
    public int AttackPower => attack;
    public int Defense => defense;
    public int Speed => speed;
    public int AttackSpeed => attackSpeed;
    public int AttackRange => attackRange;

    public Sprite Sprite => sprite;
}
