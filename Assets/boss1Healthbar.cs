using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealthBar : MonoBehaviour
{
    public BossController bossHealth;
    public Image healthBar;
    public float healthAmount;

    void Start()
    {

    }

    void Update()
    {
        UpdateHealthBar1();
    }

    void UpdateHealthBar1()
    {
        healthAmount = bossHealth.currentHealth;
        healthBar.fillAmount = healthAmount / 30f;
    }
}
