using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [Header("Preference")]
    public Transform player;
    public LayerMask groundLayer;
    public PlayerHealth playerHealth;
    public BloodSplash bloodSplash;

    [Header("Enemy Object")]
    [Range(0, 100)] public float health;
    public int maxHealth;
    public Animator animator;
    [HideInInspector] public Rigidbody2D rigid;

    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public int direction;


    [Header("Weapon")]
    public Transform weaponMaxRange;
    public Transform weaponMinRange;
    public float speedAttack;
    public float damage;
    public bool isAttacking;

    [Header("Patrol and Guard")]
    public bool playerDetected;

    #region BeAttacked
    public void BeAttacked(float damage)
    {
        health -= damage;
        bloodSplash.SpawnBlood();
        if (health <= 0) BeDeath();
    }
    public void BeDeath()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
    #endregion

    #region Debug
    public void OnDrawGizmosSelected()
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
