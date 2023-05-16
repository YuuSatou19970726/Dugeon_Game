using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyProperties : ScriptableObject
{
    #region Fields
    [HideInInspector] public Rigidbody2D objectRigid;
    [HideInInspector] public Animator animator;
    public bool isDead = false;

    [Header("Object Movement")]
    public float maxHealth;
    public float moveSpeed;
    public LayerMask groundLayer;
    public bool isGrounded;
    public bool isJumping;
    public bool isAttacking;
    public bool isHurting;
    public int faceDirection;
    public int originSpriteDirection;

    [Header("Object Attacking")]
    public float attackDamage;
    public float attackSpeed;
    public float minAttackRange;
    public float maxAttackRange;

    [Header("Object Bullet")]
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletSize;
    public int bulletCount;

    [Header("Object Guard and Patrol")]
    public float detectRange;
    public LayerMask AttackLayer;
    public float patrolRestTime;
    public float idleTime;
    #endregion

}
