using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyController : MonoBehaviour
{
    public GameObject ShieldEnemy;
    public GameObject healthRegenPowerUpPrefab;
    public float moveSpeed = 1f;
    private static int enemyCounter;
    private Transform player;
    public int enemyHealth = 3;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCounter = enemies.Length;
        Debug.Log("Initial Enemy Count: " + enemyCounter);
    }

    void Update()
    {
        // Move towards other objects tagged as "Enemy"
        GameObject[] nearbyEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in nearbyEnemies)
        {
            if (enemy != gameObject) // Exclude itself from the list
            {
                Vector2 direction = enemy.transform.position - transform.position;
                direction.Normalize();
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
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
        // Instantiate the health regeneration power-up at the last enemy's position
        Instantiate(healthRegenPowerUpPrefab, transform.position, Quaternion.identity);
        Debug.Log("Power-up Dropped!");
    }

    public static void ResetEnemyCounter()
    {
        enemyCounter = 0;
    }
}
