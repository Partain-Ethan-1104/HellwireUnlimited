using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject explosionPrefab; // Reference to the explosion prefab
    private bool hasExploded = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile hit the player or a wall
        if (!hasExploded && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall")))
        {
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