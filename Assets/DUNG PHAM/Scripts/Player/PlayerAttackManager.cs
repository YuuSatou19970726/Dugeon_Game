using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour, IDamageable
{
    public List<Collider2D> attackColliders = new List<Collider2D>();
    public ContactFilter2D filter;
    public static PlayerAttackManager instance;
    int hitCount = 0;
    Collider2D[] target = new Collider2D[5];
    PlayerMovementController playerMovementController;
    public ParticleSystem playerBlood;
    public ParticleSystem enemyBlood;
    float health;
    [SerializeField] float attackDamage;
    [SerializeField] HealthBar healthBar;

    void Awake()
    {
        instance = this;

        playerMovementController = GetComponent<PlayerMovementController>();
    }

    void Start()
    {
        health = playerMovementController.maxHealth;
        playerMovementController.isHurt = false;
        playerMovementController.isDied = false;

        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        // Debug.Log(health);

        if (Input.GetKeyDown(KeyCode.P)) GetDamage(10);

        BeDead();
    }

    public void AttackCast(int id)
    {
        int count = Physics2D.OverlapCollider(attackColliders[id].GetComponent<Collider2D>(), filter, target);

        if (count == 0) return;

        for (int x = 0; x < count; x++)
        {
            hitCount += 1;

            target[x].GetComponent<IDamageable>().GetDamage(attackDamage);
            enemyBlood.Play();

            Debug.Log(hitCount);
        }
    }

    public void GetDamage(float damage)
    {
        if (playerMovementController.isHurt) return;

        health -= damage;
        playerMovementController.isHurt = true;
        playerBlood.Play();

        healthBar.SetHealth(health);
    }

    public void BeDead()
    {
        if (health > 0) return;

        playerMovementController.isDied = true;
    }
}

