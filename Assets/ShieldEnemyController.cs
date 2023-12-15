using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyController : MonoBehaviour
{
    public GameObject ShieldEnemy;
    public GameObject healthRegenPowerUpPrefab;
    public GameObject otherPowerUpPrefab;
    public float moveSpeed = 1f;
    private static int enemyCounter;
    private Transform player;
    public int enemyHealth = 3;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCounter = enemies.Length;
    }

    void Update()
    {
        MoveTowardsEnemies();
            
        // Dies if Health Equals Zero
        if (enemyHealth <= 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyCounter = enemies.Length;
            if (enemyCounter == 1)
            {
                DropPowerUp(); // Call the method to drop power-up
            }

            Destroy(ShieldEnemy);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            enemyHealth -= 1;
        }
    }

    void DropPowerUp()
    {
        // Randomly choose between two power-ups with a 50/50 chance
        int randomChoice = Random.Range(0, 2);

        // Determine which power-up to instantiate based on the random choice
        GameObject powerUpPrefab = (randomChoice == 0) ? healthRegenPowerUpPrefab : otherPowerUpPrefab;

        // Instantiate the chosen power-up at the last enemy's position
        Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        Debug.Log("Power-up Dropped!");
    }

    public static void ResetEnemyCounter()
    {
        enemyCounter = 0;
    }

    void MoveTowardsEnemies()
    {
        GameObject[] nearbyEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in nearbyEnemies)
        {
            if (enemy != gameObject)
            {
                Vector2 direction = enemy.transform.position - transform.position;
                direction.Normalize();

                // Check if it's the same object
                if (enemy != gameObject)
                {
                    
                    // Check for obstacles in the path
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
                    if (hit.collider != null && hit.collider.CompareTag("Wall"))
                    {
                        // Reverse the direction if a wall is detected
                        direction = -direction;
                    }
                    else
                    { 
                        // Move using Transform
                        transform.position = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;
                    }

                    
                }
            }
        }
    }

}

