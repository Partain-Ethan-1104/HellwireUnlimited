using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedEnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1f;
    public GameObject projectilePrefab;
    public GameObject healthRegenPowerUpPrefab;
    public GameObject Enemy;
    private static int enemyCounter;
    public int enemyHealth = 3;
    public float shootCooldown = 2f;

    public float preferredDistance = 1f;
    public float shootTimer = 1f;
    public float fireRate = 2f;
    public float repositionDistance = 5f; // Distance to move perpendicularly
    public float repositionCooldown = 2f; // Cooldown between repositioning
    private float nextRepositionTime;

    private float nextFireTime;

    //private bool moveRight = true; // Flag to control left/right movement

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //enemyCounter = FindObjectsOfType<EnemyController>().Length;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCounter = enemies.Length;
        Debug.Log("Intial Enemy Count: " + enemyCounter);
    }

    void Update()
    {
        if (player == null)
        {
            // Player not found, return
            return;
        }

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Move away from the player if too close
        if (distanceToPlayer < preferredDistance)
        {
            Vector2 direction = transform.position - player.position;
            direction.Normalize();
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
            if (hit.collider != null && hit.collider.tag == "Wall")
            {
                // Reverse the direction if a wall is detected
                direction = -direction;
            }
        }

        // Check if it's time to reposition
        if (Time.time > nextRepositionTime)
        {
            Reposition();
            nextRepositionTime = Time.time + repositionCooldown;
        }

        // Shoot projectiles
        if (Time.time > shootTimer)
        {
            Shoot();
            shootTimer = Time.time + shootCooldown;
        }

        // Dies if Health Equals Zero
        if (enemyHealth == 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyCounter = enemies.Length;
            Debug.Log("Updated Enemy Count: " + enemyCounter);
            if (enemyCounter == 1)
            {
                DropPowerUp(); // Call the method to drop power-up
            }

            Destroy(Enemy);
           
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            enemyHealth -= 1;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Set the projectile direction to the player
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            // Set the rotation of the projectile to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += -90f;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Set the velocity of the projectile
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<ProjectileScript>().speed;

            // Reposition perpendicularly after shooting
            Reposition();
        }
    }
    void Reposition()
    {
        // Calculate direction from enemy to player
        Vector2 directionToPlayer = player.position - transform.position;
        directionToPlayer.Normalize();

        // Calculate perpendicular direction
        Vector2 perpendicularDirection = new Vector2(-directionToPlayer.y, directionToPlayer.x);

        // Check for obstacles in the current repositioning direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, perpendicularDirection, repositionDistance);
        if (hit.collider != null && hit.collider.tag == "Wall")
        {
            // If there's an obstacle, choose the opposite direction
            perpendicularDirection = -perpendicularDirection;
        }

        // Move left or right based on the perpendicular direction
        transform.Translate(perpendicularDirection * repositionDistance * 3 * moveSpeed * Time.deltaTime, Space.World);
    }
    void DropPowerUp()
    {
        // Instantiate the health regeneration power-up at the last enemy's position
        Instantiate(healthRegenPowerUpPrefab, transform.position, Quaternion.identity);
        Debug.Log("Power-up Dropped!");
    }

    public static void ResetEnemyCounter()
    {
        enemyCounter = 0;
    }

}

