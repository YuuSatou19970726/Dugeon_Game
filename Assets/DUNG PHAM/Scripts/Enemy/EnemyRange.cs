using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyProperties
{
    [Header("Weapon")]
    public Bullet bulletPrefab;
    public float bulletSpeed;
    float weaponRange;
    Bullet bullet;

    [Header("Patrol and Guard")]
    public LayerMask scanLayer;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        weaponRange = Vector2.Distance(weaponMaxRange.position, weaponMinRange.position);
        playerDetected = false;

        animator.Play("Idle");
    }
    void Update()
    {
        FaceToObject(player.position);
        FindPlayer();
    }

    #region  Guard
    void FaceToObject(Vector3 target)
    {
        if (transform.position.x > target.x)
        {
            transform.localScale = new Vector3(1, 1, 0);
            direction = -1;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 0);
            direction = 1;
        }
    }

    void FindPlayer()
    {
        RaycastHit2D target = Physics2D.Raycast(transform.position, direction * Vector2.right, weaponRange, scanLayer);

        if (target.collider)
        {
            playerDetected = true;
            Attack();
        }
        else
        {
            playerDetected = false;
        }
    }
    #endregion

    #region  Attack
    void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        animator.Play("Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed * direction;
        bullet.transform.parent = transform.parent;

        animator.Play("Idle");

        yield return new WaitForSeconds(speedAttack);

        isAttacking = false;
    }

    #endregion
}
