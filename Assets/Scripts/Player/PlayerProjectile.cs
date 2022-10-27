using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(direction, enemy.GetComponent<Rigidbody2D>());

            ApplyLifeSteal(playerStats.PlayerReference, enemy.EnemyStats.CurrentMaxHealth);

            enemy.TakeDamage(playerStats.CurrentDamage);
            Destroy(gameObject);
        }
    }

    private void ApplyKnockback(Vector2 direction, Rigidbody2D rb)
    {
        if (playerStats.KnockBack == 0) return;
        rb.AddForce(-direction * playerStats.KnockBack, ForceMode2D.Impulse);
    }

    private void ApplyLifeSteal(Player player, int enemyHealth)
    {
        if (playerStats.LifeSteal == 0) return;

        float floatAmount = enemyHealth * player.PlayerStats.LifeSteal;
        int healAmount = Mathf.RoundToInt(floatAmount);
        player.Heal(healAmount);
    }
}
