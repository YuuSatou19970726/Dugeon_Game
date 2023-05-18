using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour, IAttacker
{
    #region Variables
    EnemyController enemyController;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    List<GameObject> bullets = new List<GameObject>();
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletSize;
    [SerializeField] int bulletCount;

    #endregion


    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    void Start()
    {
        CreateObject();
    }

    void Update()
    {
        DestroyOutRangeBullet();

        if (bullets.Count > 0)
        {
            foreach (GameObject b in bullets)
            {
                BulletImpacted(b, enemyController.attackDamage);
            }
        }

    }

    #region Bullet Pool
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
            int multipleDistance = 3;

            if (distance > enemyController.maxAttackRange * multipleDistance)
            {
                bullet.SetActive(false);
            }
        }
    }
    Vector3 GetBulletSpawnPosition()
    {
        float bulletDirection = enemyController.faceDirection * enemyController.originSpriteDirection;

        float bulletSpawnDistance = enemyController.minAttackRange * bulletDirection * 0.5f;

        Vector3 spawnPosition = new Vector3(transform.position.x + bulletSpawnDistance, transform.position.y, 0);

        return spawnPosition;
    }

    Vector2 GetBulletVelocity()
    {
        return Vector2.right * bulletSpeed * enemyController.faceDirection * enemyController.originSpriteDirection;
    }

    void BulletImpacted(GameObject bullet, float damage)
    {
        if (!bullet.activeInHierarchy) return;

        Collider2D coli = Physics2D.OverlapCircle(bullet.transform.position, bulletSize / 2, enemyController.attackLayer);

        if (coli.GetComponent<IDamageable>() == null) return;

        coli.GetComponent<IDamageable>().GetDamage(damage);

        if (coli.GetComponent<IStopAttack>() != null)
            enemyController.playerDied = coli.GetComponent<IStopAttack>().StopAttack();

        bullet.gameObject.SetActive(false);
    }
    #endregion


    #region IAttacker
    public void Attack(float damage)
    {
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.transform.position = GetBulletSpawnPosition();
                bullet.GetComponent<Rigidbody2D>().velocity = GetBulletVelocity();
            }

            yield return new WaitForSeconds(enemyController.attackSpeed);
        }
    }


    #endregion
}
