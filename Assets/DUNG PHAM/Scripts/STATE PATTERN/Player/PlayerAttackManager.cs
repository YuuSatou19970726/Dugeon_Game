using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour, IDamageable
{
    PlayerController playerController;
    PlayerDatabase playerDatabase;
    public List<Collider2D> attackColliders = new List<Collider2D>();
    public ContactFilter2D filter;
    Collider2D[] target = new Collider2D[5];
    public ParticleSystem playerBlood;
    public ParticleSystem enemyBlood;
    Rigidbody2D rigid;

    float health;
    int hitCount = 0;
    float hitTimer;
    public float hurtTimer;

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerDatabase = GetComponent<PlayerDatabase>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerDatabase.healthBar.ShowMaxHealth(playerDatabase.maxHealth);
        playerDatabase.healthBar.ShowHealth(health);

        playerDatabase.isHurt = false;
        playerDatabase.isDied = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) GetDamage(10, transform);

        hitTimer += Time.deltaTime;
        hurtTimer += Time.deltaTime;
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    public void AttackCast(int id)
    {
        hitReset();

        int count = Physics2D.OverlapCollider(attackColliders[id], filter, target);

        if (count == 0) return;

        for (int x = 0; x < count; x++)
        {
            hitCount += 1;

            target[x].GetComponent<IDamageable>().GetDamage(playerDatabase.attackDamage, attackColliders[id].transform);

            enemyBlood.Play();

            // Recovery(1);
        }

        hitTimer = 0;
    }

    void hitReset()
    {
        if (hitTimer > playerDatabase.hitResetTime)
        {
            hitCount = 0;
        }
    }

    public void Recovery(float amount)
    {
        if (health >= playerDatabase.maxHealth)
        {
            health = playerDatabase.maxHealth;
            return;
        }

        health += amount;
        playerDatabase.healthBar.ShowHealth(health);
    }
    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    public void GetDamage(float damage, Transform knocker)
    {
        if (hurtTimer < 2f) return;

        hurtTimer = 0f;
        health -= damage;

        playerDatabase.isHurt = true;
        playerBlood.Play();
        playerDatabase.healthBar.ShowHealth(health);

        BeKnockBack(knocker);
    }

    void BeKnockBack(Transform knocker)
    {
        Vector2 knockWay = transform.position - knocker.position;

        int knockX = knockWay.x < 0 ? -1 : 1;
        int knockY = knockWay.y < 0 ? -2 : 2;

        knockWay = new Vector2(knockX, knockY);

        rigid.velocity = knockWay * 5f;
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float health)
    {
        this.health = health;
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

}

