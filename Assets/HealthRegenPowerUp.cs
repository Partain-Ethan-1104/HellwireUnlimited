using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenPowerUpScript : MonoBehaviour
{
    public int healthBonus = 20; // Adjust the amount of health regeneration as needed

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyPowerUp(GameObject player)
    {
        // Assuming the player has a PlayerHealth script
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healthBonus);
        }
    }
}
