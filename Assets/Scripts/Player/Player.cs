using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    private GroundCheck groundCheck;

    public PlayerStats PlayerStats { get => playerStats; }
    public Animator Animator { get => animator; }
    public Collider2D Col { get => col; }
    public Rigidbody2D Rb { get => rb; }
    public GroundCheck GroundCheck { get => groundCheck; }

    public virtual void Start()
    {
        playerStats.Execute();
        GetPlayerComponents();
    }

    public virtual void Update()
    {
        
    }

    private void GetPlayerComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    public void TakeDamage(int damageTaken)
    {
        playerStats.CurrentHealth -= damageTaken;
        ScreenShake.instance.StartShake(0.2f, 0.1f);
        UiStatManager.ChangeAmount("Health", playerStats.CurrentHealth.ToString());
        if (playerStats.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Dead");
    }
}
