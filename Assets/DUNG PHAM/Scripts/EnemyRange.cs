using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    #region Variable Declare
    [Header("Preference")]
    public Transform player;
    public LayerMask groundLayer;

    [Header("Enemy Object")]
    Rigidbody2D rigid;
    public float hitPoint;
    int faceIndex;

    [Header("Weapon")]
    public GameObject bulletPrefab;
    public float weaponRange;
    public float speedAttack;
    public float damage;
    bool isAttacking;
    public float bulletSpeed;
    GameObject bullet;

    [Header("Guard")]
    public float distanceGuard;
    Vector3 targetPos;
    public bool playerDetected;
    public LayerMask scanLayer;

    #endregion
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerDetected = false;
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
            faceIndex = -1;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 0);
            faceIndex = 1;
        }
    }

    void FindPlayer()
    {
        RaycastHit2D target = Physics2D.Raycast(transform.position, faceIndex * Vector2.right, weaponRange, scanLayer);

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
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector3.right * bulletSpeed * faceIndex;
        bullet.transform.parent = transform;

        yield return new WaitForSeconds(speedAttack);

        isAttacking = false;
    }

    #endregion

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + faceIndex * Vector3.right * weaponRange);
    }
    #endregion
}
