using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Text healthText; // Reference to a UI Text element

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();

        // Check if the player has run out of health
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        // Increase the player's health, but don't exceed the maximum health
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);

        // Update the UI
        UpdateHealthUI();
    }

    void Die()
    {
        // Perform actions when the player dies, e.g., play death animation, respawn, etc.
        Debug.Log("Player has died");
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }
}
