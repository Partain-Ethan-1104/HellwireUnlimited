using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private float dashTimer = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            Dash();
        }

        if (isDashing)
        {
            DashMovement();
        }
    }

    void Dash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        
        // For simplicity, we'll disable the player's collider during the dash
        GetComponent<Collider2D>().enabled = false;

        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse pointer
        Vector2 dashDirection = (mousePosition - (Vector2)transform.position).normalized;

        // Perform a raycast to check for collisions during the dash
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, dashDistance, LayerMask.GetMask("Wall"));

        // If there is a collision, adjust the dash distance
        if (hit.collider != null)
        {
            dashDistance = hit.distance;

            // Adjust the player position to the hit point
            transform.position = hit.point;
        }


        // Move the player towards the mouse position
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, dashDistance);
    }

    void DashMovement()
    {
        if (dashTimer > 0)
        {
            // Calculate the movement distance for this frame
            float moveDistance = dashDistance * Time.deltaTime / dashDuration;

            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the direction from the player to the mouse pointer
            Vector2 dashDirection = (mousePosition - (Vector2)transform.position).normalized;

            // Perform a raycast to check for collisions during the dash
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, moveDistance, LayerMask.GetMask("Walls"));

            // If there is a collision, adjust the move distance
            if (hit.collider != null)
            {
                moveDistance = hit.distance;
            }

            // Calculate the new position based on the adjusted move distance
            Vector2 newPosition = (Vector2)transform.position + dashDirection * moveDistance;

            // Move the player using Rigidbody2D.MovePosition
            GetComponent<Rigidbody2D>().MovePosition(newPosition);

            // Check for collisions manually using Physics2D.OverlapCircle
            Collider2D wallCollider = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Walls"));

            // If there is a collision, stop the dash
            if (wallCollider != null)
            {
                isDashing = false;
                dashDistance = 5f; // Reset dash distance
                GetComponent<Collider2D>().enabled = true; // Re-enable the collider
                GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset velocity
                return;
            }

            dashTimer -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            dashDistance = 5f; // Reset dash distance
            GetComponent<Collider2D>().enabled = true; // Re-enable the collider
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset velocity
        }
    }







}
