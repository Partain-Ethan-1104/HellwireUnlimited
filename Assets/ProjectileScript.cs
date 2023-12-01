using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 8f;

    void Start()
    {
        // Add a Rigidbody2D component if not already attached
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Set the Rigidbody2D properties
        rb.gravityScale = 0;  // No gravity for projectiles
        rb.velocity = transform.up * speed;

        // Rotate the sprite to match the direction
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        angle += -90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Destroy the projectile after a certain time (adjust as needed)
        Destroy(gameObject, 100f);
    }
}
