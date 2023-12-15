using System.Collections;
using System.Collections.Generic;
// ExtraBulletPerShotPowerUp.cs
using UnityEngine;

public class ExtraBulletPerShotPowerUp : MonoBehaviour
{
    public float spreadAngle = 15f; // Adjust the spread angle as needed

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
        }
    }

    void ApplyPowerUp(GameObject player)
    {
        // Check if the player has the Shooting component on itself or its children
        Shooting shootingScript = player.GetComponentInChildren<Shooting>();
        if (shootingScript != null)
        {
            // Increase the number of bullets per shot and set the spread angle
            shootingScript.IncreaseBulletsPerShot();
            shootingScript.SetBulletSpreadAngle(spreadAngle);
        }

        // Destroy the power-up object after it's picked up
        Destroy(gameObject);
    }
}
