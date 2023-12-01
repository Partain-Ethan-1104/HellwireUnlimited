using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    //public float rotationSpeed = 1f;
    public GameObject projectilePrefab;
    public GameObject Enemy;
    public float shootCooldown = 2f;
    public GameObject healthRegenPowerUpPrefab; // Reference to the health regeneration power-up prefab

    private static int enemyCounter; // Counter to track the number of enemies in the room
    private Transform player;
    public float shootTimer = 1f;
    public int enemyHealth = 3;
    public int playerReactivity = 50;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //enemyCounter = FindObjectsOfType<EnemyController>().Length;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCounter = enemies.Length;
        Debug.Log("Initial Enemy Count: " + enemyCounter);
    }


    void Update()
    {
        // Move towards the player
        float playerDistance = (transform.position - player.position).sqrMagnitude;  
        if (playerDistance <= playerReactivity)
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

            
            // Shoot projectiles
            if (Time.time > shootTimer)
            {
                Shoot();
                shootTimer = Time.time + shootCooldown;
            }
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
        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the projectile direction to the player (adjust as needed)
        Vector2 direction = player.position - transform.position;
        direction.Normalize();

        // Set the rotation of the projectile to face the player, adjusted by 90 degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += -90f;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set the velocity of the projectile
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<ProjectileScript>().speed;
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
