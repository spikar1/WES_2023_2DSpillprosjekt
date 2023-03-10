using UnityEngine;

[CreateAssetMenu(fileName = "New LUG Stats", menuName = "LUG/Create new LUG Stats")]
public class LUG_EntityStats : ScriptableObject
{
    [SerializeField] private int speed = 5;
    [SerializeField] private int attackPower = 5;
    [SerializeField] private int defense = 5;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float attackRange = 1.5f;


    public int Speed => speed;
    public int AttackPower => attackPower;
    public int Defense => defense;
    public int MaxHealth => maxHealth;
    public float AttackRange => attackRange;

}
