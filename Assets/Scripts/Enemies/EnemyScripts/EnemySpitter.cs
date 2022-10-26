using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpitter : Enemy
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform emitter;

    private Vector2 movement;
    private bool permanentStop;

    private float attackCd;
    public override void Start()
    {
        base.Start();
        attackCd = RandomAttackInterval(EnemyStats.CurrentAttackSpeed);
        permanentStop = false;
    }

    public override void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        RotateCharacter(direction);
        direction = direction.normalized;
        movement = direction;
        Shoot();
    }

    public override void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void MoveCharacter(Vector2 direction)
    {
        if (permanentStop)
            return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 4)
        {
            permanentStop = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemyStats.CurrentSpeed * Time.deltaTime);
        }
    }

    private void RotateCharacter(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void Shoot()
    {
        if(attackCd <= 0f)
        {
            Vector3 shootDirection = player.transform.position - transform.position;
            GameObject projectileInstance = Instantiate(projectile, emitter.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectileInstance.GetComponent<Rigidbody2D>();
            projectileRb.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * EnemyStats.CurrentProjectileSpeed;
            attackCd = RandomAttackInterval(EnemyStats.CurrentAttackSpeed);
        }
        attackCd -= Time.deltaTime;
    }

    private float RandomAttackInterval(float enemyAttackSpeed)
    {
        return Random.Range(EnemyStats.CurrentAttackSpeed, EnemyStats.CurrentAttackSpeed * 1.5f);
    }

    public override void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
