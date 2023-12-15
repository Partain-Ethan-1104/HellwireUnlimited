using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Controller : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public GameObject missilePrefab;
    public GameObject projectilePrefab;
    public GameObject Boss2;
    public float missileCooldown = 2.5f;
    public int bossHealth = 10;
    public int currentHealth;
    public float shootReadyCooldown = 10f;
    public float shootCooldown = 3f;
    public float shootDuration = 1f;
    public float projectileSpeedMult = 2f;


    private Transform player;

    private float missileTimer = 0f;
    private float shootReadyTimer = 0f;
    private float shootTimer = 0f;
    private bool isShooting = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = bossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the player
        float playerDistance = (transform.position - player.position).sqrMagnitude;
        if (playerDistance <= 500)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // Check for obstacles in the path
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
            if (hit.collider != null && hit.collider.tag == "Wall")
            {
                // Reverse the direction if a wall is detected
                direction = -direction;
            }

            if (Time.time > missileTimer)
            {
                shootMissile();
                missileTimer = Time.time + missileCooldown;
            }

            if (Time.time > shootReadyTimer)
            {
                startShoot();
                shootReadyTimer = Time.time + shootReadyCooldown;
            }
        }

        if (isShooting)
        {
            shootContinuous();
        }

        if (bossHealth <= 0)
        {
            Destroy(Boss2);
        }
    }


    void shootMissile()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
    }

    void startShoot()
    {
        isShooting = true;
        shootTimer = Time.time + shootDuration;
    }
    void shootContinuous()
    {
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the projectile direction to the player (adjust as needed)
        Vector2 direction = player.position - transform.position;
        direction.Normalize();

        // Set the rotation of the projectile to face the player, adjusted by 90 degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += -90f;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set the speed of the projectile
        float speed = projectile.GetComponent<ProjectileScript>().speed * projectileSpeedMult;

        // Set the velocity of the projectile
        projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        
        if (Time.time > shootTimer)
        {
            isShooting = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            bossHealth -= 1;
            currentHealth = bossHealth;
        }
    }

}
