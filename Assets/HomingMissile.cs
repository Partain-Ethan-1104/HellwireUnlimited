using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float missileSpeed = 150f;
    public GameObject explosionPrefab;

    private Transform player;
    private Rigidbody2D rb;
    private bool hasExploded;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = (transform.position - player.position).sqrMagnitude;
        if (playerDistance <= 1000)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            rb.AddForce(direction * missileSpeed);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += -90f;
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(35);
                }
            }

            Explode();
        }
    }

    void Explode()
    {
        // Check if the projectile still exists
        if (gameObject != null)
        {
            // Instantiate explosion prefab at the projectile's position
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destroy the projectile
            Destroy(gameObject);

            // Destroy the explosion prefab after its animation (you may adjust the time as needed)
            Destroy(explosion, 0.2f);
        }

        // Set the flag to indicate that the explosion has occurred
        hasExploded = true;
    }
}
