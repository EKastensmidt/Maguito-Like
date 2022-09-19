using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEater : Enemy
{
    private Vector2 movement;
    [SerializeField] private float knockbackForce;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        RotateCharacter(direction);
        direction = direction.normalized;
        movement = direction;
    }
    public override void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            ApplySelfKnockback(collision);
            player.TakeDamage(EnemyStats.CurrentDamage);
        }
    }

    private void RotateCharacter(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void MoveCharacter(Vector2 direction)
    {
        //rb.MovePosition((Vector2)transform.position + (direction * EnemyStats.CurrentSpeed * Time.deltaTime));
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemyStats.CurrentSpeed * Time.deltaTime);
    }

    public override void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        EnemyManager.enemiesToSpawn.RemoveAt(0);
        UiStatManager.ChangeAmount("CurrentEnemies", EnemyManager.enemiesToSpawn.Count.ToString());
        EnemyManager.CheckEnemyList();
        Destroy(gameObject);
    }

    private void ApplySelfKnockback(Collision2D collision)
    {
        Vector2 difference = (transform.position - collision.gameObject.transform.position).normalized;
        Vector2 force = difference * knockbackForce;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
