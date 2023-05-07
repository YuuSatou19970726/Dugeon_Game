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
    void Awake()
    {
        instance = this;

        playerMovementController = GetComponent<PlayerMovementController>();
    }

    void Start()
    {
        health = playerMovementController.playerDatabase.maxHealth;
        playerMovementController.playerDatabase.isHurt = false;
        playerMovementController.playerDatabase.isDied = false;
    }
    public void AttackCast(int id)
    {
        int count = Physics2D.OverlapCollider(attackColliders[id].GetComponent<Collider2D>(), filter, target);

        if (count == 0) return;

        for (int x = 0; x < count; x++)
        {
            hitCount += 1;
            enemyBlood.Play();

            Debug.Log(hitCount);
        }
    }




    void Update()
    {
        Debug.Log(health);

        if (Input.GetKeyDown(KeyCode.P)) GetDamage(10);

        BeDead();
    }
    public void GetDamage(float damage)
    {
        if (playerMovementController.playerDatabase.isHurt) return;

        health -= damage;
        playerMovementController.playerDatabase.isHurt = true;
        playerBlood.Play();
    }
    public void BeDead()
    {
        if (health > 0) return;

        playerMovementController.playerDatabase.isDied = true;
    }
}

