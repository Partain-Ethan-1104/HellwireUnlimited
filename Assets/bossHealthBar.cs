using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealthBar2 : MonoBehaviour
{
    public Boss2Controller bossHealth;
    public Image healthBar;
    public float healthAmount;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthAmount = bossHealth.currentHealth;
        healthBar.fillAmount = healthAmount / 30f;
    }
}
