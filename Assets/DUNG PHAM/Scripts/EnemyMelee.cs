using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    #region Variable Declare
    [Header("Preference")]
    public Transform player;
    public LayerMask groundLayer;

    [Header("Enemy Object")]
    Rigidbody2D rigid;
    public float hitPoint;

    [Header("Movement")]
    public float speed;
    public float jumpForce;

    [Header("Weapon")]
    public Transform weaponMaxRange, weaponMinRange;
    public float speedAttack;
    public float damage;
    bool isAttacking;

    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float distanceGuard;
    Vector3 targetPos;
    public bool playerDetected;

    #endregion
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        targetPos = pointA.position;
        playerDetected = false;
    }
    void Update()
    {      
        FindPlayer();

        if (playerDetected)
        {
            MeleeAttack();
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
    IEnumerator PatrolRest(Vector2 target)
    {
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        targetPos = target;
    }
    #endregion

    #region Movement
    void MoveToObject(Vector3 target)
    {
        if (transform.position.x > target.x)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

        if (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if (GroundCheck() && WallCheck())
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    bool GroundCheck()
    {
        Collider2D coli = GetComponent<Collider2D>();
        Vector3 lowestPoint = coli.bounds.center - new Vector3(0f, coli.bounds.extents.y, 0f);

        if (Physics2D.OverlapCircle(lowestPoint, 0.1f, groundLayer))
        {
            return true;
        }
        else return false;
    }
    bool WallCheck()
    {
        if (Physics2D.OverlapCircle(weaponMaxRange.position, 0.2f, groundLayer))
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
    void MeleeAttack()
    {
        if (!isAttacking)
        {
            Collider2D target = Physics2D.OverlapArea(weaponMinRange.position, weaponMaxRange.position);
            if (target.gameObject.tag == "Player")
            {
                isAttacking = true;
                StartCoroutine(AttackDelay());
            }
        }
    }
    IEnumerator AttackDelay()
    {
        Debug.Log("Hello");
        yield return new WaitForSeconds(speedAttack);
        isAttacking = false;
    }
    #endregion

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Collider2D coli = GetComponent<Collider2D>();
        Vector3 lowestPoint = coli.bounds.center - new Vector3(0f, coli.bounds.extents.y, 0f);

        Gizmos.DrawWireSphere(lowestPoint, 0.1f);

        Gizmos.DrawLine(weaponMaxRange.position, weaponMinRange.position);

        Gizmos.DrawWireSphere(weaponMaxRange.position, 0.2f);
    }
    #endregion
}
