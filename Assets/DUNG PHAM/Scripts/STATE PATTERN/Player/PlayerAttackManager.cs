using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour, IDamageable
{
    public static PlayerAttackManager instance;
    PlayerController playerController;
    PlayerDatabase playerDatabase;
    public List<Collider2D> attackColliders = new List<Collider2D>();
    public ContactFilter2D filter;
    Collider2D[] target = new Collider2D[5];
    public ParticleSystem playerBlood;
    public ParticleSystem enemyBlood;
    float health;
    int hitCount = 0;
    float hitTimer;

    void Awake()
    {
        instance = this;

        playerController = GetComponent<PlayerController>();
        playerDatabase = GetComponent<PlayerDatabase>();
    }

    void Start()
    {
        health = playerDatabase.maxHealth;
        playerDatabase.isHurt = false;
        playerDatabase.isDied = false;

        playerDatabase.healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) GetDamage(10);

        BeDead();

        hitTimer += Time.deltaTime;
    }

    public void AttackCast(int id)
    {
        hitReset();

        int count = Physics2D.OverlapCollider(attackColliders[id].GetComponent<Collider2D>(), filter, target);

        if (count == 0) return;

        for (int x = 0; x < count; x++)
        {
            hitCount += 1;

            target[x].GetComponent<IDamageable>().GetDamage(playerDatabase.attackDamage);
            enemyBlood.Play();
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

    public void GetDamage(float damage)
    {
        if (playerDatabase.isHurt) return;

        health -= damage;
        playerDatabase.isHurt = true;
        playerBlood.Play();

        playerDatabase.healthBar.SetHealth(health);

        StartCoroutine(BeDamagedDelay());
    }

    Rigidbody2D rigid;
    void BeKnockBack(Vector2 knockWay)
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(knockWay.x, rigid.velocity.y + knockWay.y);
    }

    IEnumerator BeDamagedDelay()
    {
        yield return new WaitForSeconds(1f);
        playerDatabase.isHurt = false;
    }

    public void BeDead()
    {
        if (health > 0) return;

        playerDatabase.isDied = true;
    }

    public float GetHealth()
    {
        return health;
    }
}

