using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player object is tagged as 'Player'.");
        }
    }

    void Update()
    {
        RotateTowardsPlayer();
    }

    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Adjust the rotation by 90 degrees counterclockwise
            Quaternion targetRotation = Quaternion.AngleAxis(angle - -90f, Vector3.forward);

            transform.rotation = targetRotation;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            // Handle the collision with the player's bullet
            // For example, disable the bullet or apply damage to the shield
            Destroy(collision.gameObject);
        }
    }
}
