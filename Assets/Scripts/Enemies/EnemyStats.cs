using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/Enemy", order = 0)]

public class EnemyStats : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    [SerializeField] private float attackSpeed = 0;
    [SerializeField] private float projectileSpeed = 0;

    [SerializeField] private EnemyRarity rarity;
    [SerializeField] private EnemyType type;
    [SerializeField] private int difficultyLvl = 1;

    private int currentHealth;
    private int currentMaxHealth;
    private int currentDamage;
    private float currentSpeed;

    private float currentAttackSpeed;
    private float currentProjectileSpeed;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int CurrentMaxHealth { get => currentMaxHealth; set => currentMaxHealth = value; }
    public int CurrentDamage { get => currentDamage; set => currentDamage = value; }
    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }

    public float CurrentAttackSpeed { get => currentAttackSpeed; set => currentAttackSpeed = value; }
    public float CurrentProjectileSpeed { get => currentProjectileSpeed; set => currentProjectileSpeed = value; }

    public EnemyType Type { get => type; }
    public int DifficultyLvl { get => difficultyLvl; set => difficultyLvl = value; }

    public void Execute()
    {
        currentHealth = health;
        currentMaxHealth = health;
        currentDamage = damage;
        currentSpeed = speed;
        if (type == EnemyType.ranged)
        {
            currentAttackSpeed = attackSpeed;
            currentProjectileSpeed = projectileSpeed;
        }
    }

    public enum EnemyType { melee, ranged}
    public enum EnemyRarity { common = 60, uncommon = 25, rare = 10, veryRare = 4, UltraRare = 1 }

}
