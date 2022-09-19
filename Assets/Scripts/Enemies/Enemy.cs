using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    protected Player player;
    protected Rigidbody2D rb;
    protected int currentHealth;
    public EnemyStats EnemyStats { get => enemyStats; }
    
    public virtual void Start()
    {
        enemyStats.Execute();
        currentHealth = enemyStats.CurrentHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void TakeDamage(int damageTaken)
    {
        
    }

    public virtual void Die()
    {
        
    }
}