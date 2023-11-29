using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    //public float rotationSpeed = 1f;
    public GameObject projectilePrefab;
    public GameObject Enemy;
    public float shootCooldown = 2f;

    private Transform player;
    public float shootTimer = 1f;
    public int enemyHealth = 3;
    public int playerReactivity = 50;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

            // Rotate towards the player
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);

            // Shoot projectiles
            if (Time.time > shootTimer)
            {
                Shoot();
                shootTimer = Time.time + shootCooldown;
            }
        }
        
        // Dies if Health Equals Zero
        if (enemyHealth == 0) {
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
}
