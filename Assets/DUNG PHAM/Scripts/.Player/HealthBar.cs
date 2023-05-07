using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient healthBarFill;
    public Image fill;
    public PlayerBeAttacked playerBeAttacked;
    void Awake()
    {
        playerBeAttacked = FindObjectOfType<PlayerBeAttacked>();
    }
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
        healthBar.value = playerBeAttacked.currentHealth;
        fill.color = healthBarFill.Evaluate(healthBar.normalizedValue);
    }

    public void SetMaxHealth()
    {
        healthBar.maxValue = playerBeAttacked.playerController.playerProperties.maxHealth;
        healthBar.value = playerBeAttacked.playerController.playerProperties.maxHealth;
        fill.color = healthBarFill.Evaluate(1f);
    }
}
