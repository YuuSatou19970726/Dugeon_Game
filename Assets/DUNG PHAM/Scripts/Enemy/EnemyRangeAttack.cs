using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    EnemyController enemyController;
    EnemyAnimation enemyAnimation;
    List<GameObject> bullets = new List<GameObject>();
    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        enemyAnimation = GetComponent<EnemyAnimation>();
    }
    void Start()
    {
        CreateObject();

        enemyController.enemyProperties.canAttack = true;
        enemyController.enemyProperties.isAttacking = false;
    }

    void Update()
    {
        DestroyOutRangeBullet();

        if (enemyController.enemyProperties.isDead) return;

        Attack();
    }
    void CreateObject()
    {
        for (int i = 0; i < enemyController.enemyProperties.bulletCount; i++)
        {
            GameObject bullet = Instantiate(enemyController.enemyProperties.bulletPrefab);
            // bullet.transform.parent = transform;
            bullet.transform.localScale = Vector3.one * enemyController.enemyProperties.bulletSize;
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }
    GameObject LoadObject()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy) return bullet;
        }
        return null;
    }
    void Attack()
    {
        if (!enemyController.enemyProperties.isAttacking) return;

        enemyController.enemyProperties.objectRigid.velocity = Vector2.zero;

        GameObject bullet = LoadObject();

        if (bullet)
        {
            bullet.SetActive(true);
            Vector3 spawnPosition = new Vector3(transform.position.x + /*enemyController.enemyProperties.minAttackRange * */enemyController.enemyProperties.faceDirection * enemyController.enemyProperties.originSpriteDirection, transform.position.y, 0);

            bullet.transform.position = spawnPosition;

            bullet.GetComponent<IAttackable>().GetInitDamage(enemyController.enemyProperties.attackDamage);

            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * enemyController.enemyProperties.bulletSpeed * enemyController.enemyProperties.faceDirection * enemyController.enemyProperties.originSpriteDirection;
        }

        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        enemyController.enemyProperties.canAttack = false;
        enemyController.enemyProperties.isAttacking = false;

        yield return new WaitForSeconds(enemyController.enemyProperties.attackSpeed);

        enemyController.enemyProperties.canAttack = true;
    }
    void DestroyOutRangeBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            float distance = Vector2.Distance(bullet.transform.position, transform.position);
            if (distance > enemyController.enemyProperties.maxAttackRange) bullet.SetActive(false);
        }
    }
}
