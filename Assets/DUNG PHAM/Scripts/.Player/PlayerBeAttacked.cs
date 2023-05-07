using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeAttacked : MonoBehaviour, IDamageable
{
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public float currentHealth;
    [SerializeField] ParticleSystem playerBlood;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        currentHealth = playerController.playerProperties.maxHealth;

        playerBlood.Stop();
    }
    void Update()
    {
        if (currentHealth <= 0)
        {
            playerController.playerProperties.isDead = true;
            playerBlood.Stop();
        }
    }
    public void Recovery(float number)
    {
        if (currentHealth + number > playerController.playerProperties.maxHealth)
            currentHealth = playerController.playerProperties.maxHealth;
        else currentHealth += number;
    }
    public void GetDamage(float damage)
    {
        playerController.playerProperties.isHurt = true;

        playerBlood.Play();

        if (currentHealth <= 0) { currentHealth = 0; }
        else currentHealth -= damage;
    }
}
