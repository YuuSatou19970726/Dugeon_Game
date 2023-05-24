using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour, IAttacker
{
    #region VARIABLES
    EnemyController enemyController;
    EnemyDatabase enemyDatabase;
    Collider2D enemyColi;

    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] float bulletSize = 1;
    [SerializeField] int multipleDistance = 3;
    [SerializeField] ContactFilter2D targetFilter;

    [SerializeField] int bulletCount;
    [SerializeField] List<GameObject> bullets = new List<GameObject>();
    List<Collider2D> hits = new List<Collider2D>();
    #endregion


    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        enemyDatabase = GetComponent<EnemyDatabase>();
        enemyColi = GetComponent<Collider2D>();
    }
    void Start()
    {
        bulletCount = enemyDatabase.attackNumber;
        CreateObject();
    }

    void Update()
    {
        DestroyOutRangeBullet();

        foreach (GameObject b in bullets)
        {
            BulletImpacted(b, enemyDatabase.attackDamage);
        }
    }

    #region BULLET POOL
    void CreateObject()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            bullet.transform.parent = transform.parent;
            bullet.transform.localScale = Vector3.one * bulletSize;
            bullet.SetActive(false);

            bullets.Add(bullet);
        }
    }
    void DestroyOutRangeBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            float distance = Vector2.Distance(bullet.transform.position, transform.position);

            if (distance > enemyDatabase.maxAttackRange * multipleDistance)
            {
                bullet.SetActive(false);
            }
        }
    }
    Vector2 GetBulletSpawnPosition()
    {
        float bulletDirection = enemyController.faceDirection * enemyDatabase.originSpriteDirection;

        float bulletSpawnDistance = enemyDatabase.minAttackRange * bulletDirection * 0.5f;

        Vector2 spawnPosition = new Vector2(transform.position.x + bulletSpawnDistance, transform.position.y);

        return spawnPosition;
    }

    Vector2 GetBulletVelocity()
    {
        Vector2 velocity = Vector2.right * bulletSpeed * enemyController.faceDirection * enemyDatabase.originSpriteDirection;

        return velocity;
    }

    void BulletImpacted(GameObject bullet, float damage)
    {
        if (!bullet.activeInHierarchy) return;

        int count = Physics2D.OverlapCollider(bullet.GetComponent<Collider2D>(), targetFilter, hits);

        foreach (Collider2D coli in hits)
        {
            if (coli == enemyColi) continue;

            if (coli.GetComponent<IStopAttack>() != null)
                enemyController.playerDied = coli.GetComponent<IStopAttack>().StopAttack();

            if (coli.GetComponent<IDamageable>() != null)
            {
                coli.GetComponent<IDamageable>().GetDamage(damage);
                bullet.gameObject.SetActive(false);
            }

            if (coli.gameObject.layer == LayerMask.NameToLayer("Ground"))
                bullet.gameObject.SetActive(false);
        }
    }
    #endregion


    #region IAttacker
    public void Attack()
    {
        StartCoroutine(AttackLaunchCoroutine());
    }
    IEnumerator AttackLaunchCoroutine()
    {
        // Delay to fix with animation
        yield return new WaitForSeconds(1.2f);

        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.transform.position = GetBulletSpawnPosition();
                bullet.GetComponent<Rigidbody2D>().velocity = GetBulletVelocity();
            }

            yield return new WaitForSeconds(enemyDatabase.timePerAttack);
        }

        enemyController.RunAttackCooldownCoroutine();
    }


    #endregion
}
