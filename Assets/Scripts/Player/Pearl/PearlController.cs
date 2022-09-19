using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PearlController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 pos;
    private float angle;
    [SerializeField] private float distance = 0.25f, offset = 1.5f;
    private PlayerStats playerStats;
    private float projectileSpawnCd = 0;

    private void Start()
    {
        playerStats = player.GetComponent<Player>().PlayerStats;
    }

    private void Update()
    {
        PearlMovement();

        if (Input.GetKey(KeyCode.Mouse0) && projectileSpawnCd <= 0)
        {
            Shoot();
            projectileSpawnCd = playerStats.CurrentAttackSpeed;
        }
        projectileSpawnCd -= Time.deltaTime; 
    }

    private void PearlMovement()
    {
        pos = Input.mousePosition;
        pos.z = (player.transform.position.z - Camera.main.transform.position.z);
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos = pos - player.transform.position;
        angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        if (angle < 0.0f) angle += 360.0f;
        transform.localEulerAngles = new Vector3(0, 0, angle);
        float xPos = Mathf.Cos(Mathf.Deg2Rad * angle) * distance;
        float yPos = Mathf.Sin(Mathf.Deg2Rad * angle) * distance;
        transform.position = new Vector3(player.transform.position.x + xPos, player.transform.position.y / offset + yPos, 0);

        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.LookAt(new Vector3(0, 0, mousepos.z));
    }

    private void Shoot()
    {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - player.transform.position;

        GameObject projectileInstance = Instantiate(playerStats.Projectile, transform.position, Quaternion.identity);
        ScreenShake.instance.StartShake(0.1f, 0.025f);
        Rigidbody2D projectileRb = projectileInstance.GetComponent<Rigidbody2D>();

        projectileRb.velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * playerStats.CurrentProjectileSpeed;

    }
}

