using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyProperties enemyProperties;
    Vector3 groundCheckPoint;
    public bool playerDetected;
    public bool playerInRange;
    public bool canAttack;

    Rigidbody2D rigid;
    public Collider2D playerColi;
    public bool isResting = false;

    public Transform target;
    public Transform[] patrolPoints = new Transform[2];
    int index = 0;

    public float health;
    public bool isHurt;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        health = enemyProperties.maxHealth;
    }
    void Update()
    {
        CheckGround();
        FindPlayer();

        if (isResting) return;

        MoveToTarget(target.position);
    }
    public void FindPlayer()
    {
        playerColi = Physics2D.OverlapCircle(rigid.transform.position, enemyProperties.detectRange, enemyProperties.AttackLayer);

        playerDetected = playerColi != null;

        if (playerColi)
            target = playerColi.transform;
        else
            target = patrolPoints[index];
    }

    public void MoveToTarget(Vector3 target)
    {
        enemyProperties.faceDirection = Mathf.RoundToInt(Mathf.Sign(target.x - rigid.transform.position.x)) * enemyProperties.originSpriteDirection;

        rigid.transform.localScale = new Vector3(enemyProperties.faceDirection, 1, 1);

        int moveDirection = GetMoveDirection(target);

        if (Vector2.Distance(transform.position, target) > 0.5f)

            rigid.velocity = new Vector3(enemyProperties.moveSpeed * moveDirection, rigid.velocity.y, 0);

        else
        {
            index++;

            if (index >= patrolPoints.Length)
            {
                index = 0;
            }
            isResting = true;
        }
    }


    int GetMoveDirection(Vector3 target)
    {
        playerInRange = false;

        if (!playerDetected) return (rigid.transform.position.x < target.x) ? 1 : -1;

        float distance = Vector2.Distance(rigid.transform.position, target);

        if (rigid.transform.position.x < target.x)
        {
            if (distance > enemyProperties.maxAttackRange)
            {
                return 1;
            }
            else if (distance < enemyProperties.minAttackRange)
            {
                return -1;
            }
        }
        else if (rigid.transform.position.x > target.x)
        {
            if (distance > enemyProperties.maxAttackRange)
            {
                return -1;
            }
            else if (distance < enemyProperties.minAttackRange)
            {
                return 1;
            }
        }

        playerInRange = true;
        return 0;
    }

    void CheckGround()
    {
        groundCheckPoint = GetComponent<Collider2D>().bounds.center - new Vector3(0, GetComponent<Collider2D>().bounds.size.y / 2, 0);

        if (Physics2D.OverlapCircle(groundCheckPoint, 0.1f, enemyProperties.groundLayer)) enemyProperties.isGrounded = true;
        else enemyProperties.isGrounded = false;
    }

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint, 0.1f);
        Gizmos.DrawWireSphere(transform.position, enemyProperties.minAttackRange);
        Gizmos.DrawWireSphere(transform.position, enemyProperties.maxAttackRange);
        Gizmos.DrawWireSphere(transform.position, enemyProperties.detectRange);
    }
    #endregion
}
