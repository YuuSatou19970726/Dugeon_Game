using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient healthBarFill;
    public Image fill;
    public PlayerHealth playerHealth;
    void Start()
    {
        SetMaxHealth();
    }
    void Update()
    {
        DisplayHealthBar();
    }

    void DisplayHealthBar()
    {
        healthBar.value = playerHealth.health;
        fill.color = healthBarFill.Evaluate(healthBar.normalizedValue);
    }

    public void SetMaxHealth()
    {
        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
        fill.color = healthBarFill.Evaluate(1f);
    }
}
