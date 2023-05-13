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


    public Transform target;
    public Transform[] patrolPoints = new Transform[2];
    int index = 0;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        enemyProperties.isDead = false;

        target = patrolPoints[index];

    }
    void Update()
    {
        CheckGround();
        FindPlayer();
        CheckTarget();
    }
    public void FindPlayer()
    {
        playerColi = Physics2D.OverlapCircle(rigid.transform.position, enemyProperties.detectRange, enemyProperties.AttackLayer);

        playerDetected = playerColi != null;
    }
    public IEnumerator FaceToTarget(Vector3 target)
    {
        yield return new WaitForSeconds(0f);

        enemyProperties.faceDirection = Mathf.RoundToInt(Mathf.Sign(target.x - rigid.transform.position.x)) * enemyProperties.originSpriteDirection;

        rigid.transform.localScale = new Vector3(enemyProperties.faceDirection, 1, 1);
    }


    public void MoveToTarget(Vector3 target)
    {
        if (enemyProperties.isHurting || enemyProperties.isDead || enemyProperties.isAttacking)
        {
            rigid.velocity = Vector3.zero;
            return;
        }

        StartCoroutine(FaceToTarget(target));

        int moveDirection = GetMoveDirection(target);

        if (transform.position == target || !enemyProperties.isGrounded) return;

        rigid.velocity = new Vector3(enemyProperties.moveSpeed * moveDirection, rigid.velocity.y, 0);
    }


    int GetMoveDirection(Vector3 target)
    {
        // Debug.Log("hello");
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

    void CheckTarget()
    {
        if (canAttack && playerDetected && playerInRange)
        {
            enemyProperties.isAttacking = true;
        }
        // else enemyProperties.isAttacking = false;
    }

    void CheckGround()
    {
        groundCheckPoint = GetComponent<Collider2D>().bounds.center - new Vector3(0, GetComponent<Collider2D>().bounds.size.y / 2, 0);

        // if (Physics2D.OverlapCircle(groundCheckPoint, 0.1f, enemyProperties.groundLayer)) enemyProperties.isGrounded = true;
        // else enemyProperties.isGrounded = false;

        RaycastHit2D hit = Physics2D.Raycast(groundCheckPoint, Vector2.down, 0.1f, enemyProperties.groundLayer);

        if (hit) enemyProperties.isGrounded = true;
        else
        {
            enemyProperties.isGrounded = false;
        }
    }

    public void Patrol(Transform target)
    {
        if (target != patrolPoints[index]) return;

        MoveToTarget(target.position);

        if (Vector2.Distance(transform.position, target.position) > 0.5f) return;

        index++;

        if (index >= patrolPoints.Length)
        {
            index = 0;
        }

        StartCoroutine(PatrolRest(patrolPoints[index]));

    }
    IEnumerator PatrolRest(Transform targetPos)
    {
        rigid.velocity = Vector3.zero;

        yield return new WaitForSeconds(enemyProperties.patrolRestTime);

        target = targetPos;
    }

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckPoint, new Vector3(groundCheckPoint.x, groundCheckPoint.y - 0.1f, 0));
        Gizmos.DrawWireSphere(transform.position, enemyProperties.minAttackRange);
        Gizmos.DrawWireSphere(transform.position, enemyProperties.maxAttackRange);
        Gizmos.DrawWireSphere(transform.position, enemyProperties.detectRange);
    }
    #endregion
}
