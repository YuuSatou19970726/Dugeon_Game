using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyProperties
{
    [Header("Patrol and Guard")]
    public Transform pointA;
    public Transform pointB;
    public float distanceGuard;
    Vector3 targetPos;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        rigid = GetComponent<Rigidbody2D>();
        playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        bloodSplash = GetComponentInChildren<BloodSplash>();

        playerDetected = false;

        targetPos = pointA.position;
    }
    void Update()
    {
        FindPlayer();

        if (playerDetected)
        {
            Attack();
        }
        else
        {
            Patrol();
        }
    }
    #region Patrol
    void Patrol()
    {
        if (targetPos == pointA.position)
        {
            MoveToObject(targetPos);

            if (Vector2.Distance(transform.position, targetPos) < 0.5f) StartCoroutine(PatrolRest(pointB.position));
        }
        if (targetPos == pointB.position)
        {
            MoveToObject(targetPos);

            if (Vector2.Distance(transform.position, targetPos) < 0.5f) StartCoroutine(PatrolRest(pointA.position));
        }
    }
    IEnumerator PatrolRest(Vector3 target)
    {
        rigid.velocity = Vector3.zero;
        animator.Play("Idle");
        yield return new WaitForSeconds(2);
        animator.Play("Move");
        targetPos = target;
    }
    #endregion

    #region Movement
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
        
        bloodSplash.direction = direction;

        if (transform.position != target)
        {
            // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            rigid.velocity = Vector3.left * direction * speed;
        }

        if (GroundCheck() && WallCheck())
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public bool GroundCheck()
    {
        Collider2D coli = GetComponent<Collider2D>();
        Vector3 lowestPoint = coli.bounds.center - new Vector3(0f, coli.bounds.extents.y, 0f);

        if (Physics2D.OverlapCircle(lowestPoint, 0.1f, groundLayer))
        {
            return true;
        }
        else return false;

    }
    public bool WallCheck()
    {
        if (Physics2D.OverlapCircle(weaponMinRange.position, 0.2f, groundLayer))
        {
            return true;
        }
        else return false;

    }
    #endregion

    #region Attack
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
    IEnumerator AttackDelay()
    {
        playerHealth.BeDamaged(damage);
        yield return new WaitForSeconds(speedAttack);
        isAttacking = false;
    }
    #endregion
}
