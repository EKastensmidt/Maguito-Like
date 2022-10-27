using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Object/Player", order = 0)]
public class PlayerStats : ScriptableObject
{
    private Player playerReference;
    public Player PlayerReference { get => playerReference; set => playerReference = value; }

    //PLAYER STATS
    [SerializeField] private float speed = 4;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float jumpForce = 4;

    //PROJECTILE STATS
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileAttackSpeed = 1;
    [SerializeField] private float projectileSpeed = 8;
    [SerializeField] private int projectileDamage = 10;
    [SerializeField] private float projectileSize = 1;
    [SerializeField] private float critChance = 0;

    private int currentHealth;
    private int currentMaxHealth;
    private int currentDamage;
    private float currentSpeed;
    private float currentJump;
    private float currentAttackSpeed;
    private float currentProjectileSpeed;
    private float currentProjectileSize;
    private float currentCritChance;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int CurrentDamage { get => currentDamage; set => currentDamage = value; }
    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public float CurrentJump { get => currentJump; set => currentJump = value; }
    public float CurrentAttackSpeed { get => currentAttackSpeed; set => currentAttackSpeed = value; }
    public float CurrentProjectileSpeed { get => currentProjectileSpeed; set => currentProjectileSpeed = value; }
    public float CurrentProjectileSize { get => currentProjectileSize; set => currentProjectileSize = value; }
    public GameObject Projectile { get => projectile; set => projectile = value; }
    public float CurrentCritChance { get => currentCritChance; set => currentCritChance = value; }
    public int CurrentMaxHealth { get => currentMaxHealth; set => currentMaxHealth = value; }

    //OTHER STATS
    private float knockBack;
    private float lifeSteal;
    public float KnockBack { get => knockBack; set => knockBack = value; }
    public float LifeSteal { get => lifeSteal; set => lifeSteal = value; }

    public void Execute(Player player)
    {
        playerReference = player;

        currentHealth = maxHealth;
        currentMaxHealth = maxHealth;
        currentDamage = projectileDamage;
        currentSpeed = speed;
        currentJump = jumpForce;
        currentAttackSpeed = projectileAttackSpeed;
        currentProjectileSpeed = projectileSpeed;
        currentProjectileSize = projectileSize;
        currentCritChance = critChance;

        UiStatManager.ChangeAmount("Health", currentHealth.ToString());
        UiStatManager.ChangeAmount("MaxHealth", currentMaxHealth.ToString());
        UiStatManager.ChangeAmount("Damage", currentDamage.ToString());
        UiStatManager.ChangeAmount("Speed", currentSpeed.ToString());
        UiStatManager.ChangeAmount("AttackSpeed", currentAttackSpeed.ToString());
        UiStatManager.ChangeAmount("ProjectileSpeed", currentProjectileSpeed.ToString());

        knockBack = 0;
        lifeSteal = 0;
    }
}
