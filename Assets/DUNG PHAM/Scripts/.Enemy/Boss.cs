using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyProperties
{
    public float distanceGuard;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        rigid = GetComponent<Rigidbody2D>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();

        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();

        if (playerDetected)
        {
            Attack();
        }
    }
    void FindPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < distanceGuard)
        {
            playerDetected = true;
            MoveToObject(player.position);
        }
        else
        {
            playerDetected = false;
            animator.Play("000");
        }
    }
    void MoveToObject(Vector3 target)
    {
        if (transform.position.x > target.x)
        {
            direction = 1;
        }
        else if (transform.position.x < target.x)
        {
            direction = -1;
        }

        transform.localScale = new Vector3(direction, 1, 0);

        if (transform.position != target)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) animator.Play("030");
            rigid.velocity = new Vector3(-direction * speed, rigid.velocity.y, 0);
        }
    }
    void Attack()
    {
        if (!isAttacking)
        {
            Collider2D target = Physics2D.OverlapArea(weaponMinRange.position, weaponMaxRange.position);
            if (!target) return;
            if (target.gameObject.tag == "Player")
            {
                isAttacking = true;
                StartCoroutine(AttackDelay());
            }
        }
    }
    public override void BeDeath()
    {
        animator.Play("070");
    }
    IEnumerator AttackDelay()
    {
        animator.Play("235");
        playerHealth.BeDamaged(damage);
        yield return new WaitForSeconds(speedAttack);
        isAttacking = false;
    }
}
