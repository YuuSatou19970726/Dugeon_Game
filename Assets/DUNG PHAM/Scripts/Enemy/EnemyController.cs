using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyProperties enemyProperties;
    Vector3 groundCheckPoint;
    void Start()
    {
        enemyProperties.objectRigid = GetComponent<Rigidbody2D>();
        enemyProperties.isDead = false;
    }
    void Update()
    {
        CheckGround();
        FindPlayer();
        CheckTarget();

    }
    public void FindPlayer()
    {
        Collider2D playerColi = Physics2D.OverlapCircle(enemyProperties.objectRigid.transform.position, enemyProperties.detectRange, enemyProperties.AttackLayer);

        enemyProperties.playerDetected = playerColi != null;

        if (enemyProperties.playerDetected)
        {
            MoveToTarget(playerColi.transform.position);
        }
    }
    public IEnumerator FaceToTarget(Vector3 target)
    {
        yield return new WaitForSeconds(0f);

        enemyProperties.faceDirection = Mathf.RoundToInt(Mathf.Sign(target.x - enemyProperties.objectRigid.transform.position.x)) * enemyProperties.originSpriteDirection;

        enemyProperties.objectRigid.transform.localScale = new Vector3(enemyProperties.faceDirection, 1, 1);
    }


    public void MoveToTarget(Vector3 target)
    {
        if (enemyProperties.isHurting || enemyProperties.isDead || enemyProperties.isAttacking)
        {
            enemyProperties.objectRigid.velocity = Vector3.zero;
            return;
        }

        StartCoroutine(FaceToTarget(target));

        int moveDirection = GetMoveDirection(target);

        if (enemyProperties.objectRigid.transform.position == target || !enemyProperties.isGrounded) return;

        enemyProperties.objectRigid.velocity = new Vector3(enemyProperties.moveSpeed * moveDirection, enemyProperties.objectRigid.velocity.y, 0);
    }


    int GetMoveDirection(Vector3 target)
    {
        enemyProperties.playerInRange = false;

        if (!enemyProperties.playerDetected) return (enemyProperties.objectRigid.transform.position.x < target.x) ? 1 : -1;

        float distance = Vector2.Distance(enemyProperties.objectRigid.transform.position, target);

        if (enemyProperties.objectRigid.transform.position.x < target.x)
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
        else if (enemyProperties.objectRigid.transform.position.x > target.x)
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

        enemyProperties.playerInRange = true;
        return 0;
    }

    void CheckTarget()
    {
        if (enemyProperties.canAttack && enemyProperties.playerDetected && enemyProperties.playerInRange)
        {
            enemyProperties.isAttacking = true;
        }
        // else enemyProperties.isAttacking = false;
    }

    void CheckGround()
    {
        groundCheckPoint = enemyProperties.objectRigid.GetComponent<Collider2D>().bounds.center - new Vector3(0, enemyProperties.objectRigid.GetComponent<Collider2D>().bounds.size.y / 2, 0);

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
