using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDatabase : MonoBehaviour
{
    [Header("Miscellaneous")]
    public int originSpriteDirection = -1;
    public GameObject healthBar;
    public GameObject healthBarHolder;
    public float maxHealth = 100f;
    [HideInInspector] public float health;
    float healthRate;

    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("Attack")]
    public float attackDamage = 5f;
    public int attackNumber = 1;
    public float timePerAttack = 0.1f;  // Delay between each attack
    public float attackDelay = 0.1f;    // Delay to fix with animation
    public float attackCooldown = 5f;
    public float minAttackRange = 2;
    public float maxAttackRange = 5;
    public ContactFilter2D targetFilter;

    [Header("Guard and Patrol")]
    public LayerMask guardLayer;
    public float detectRange = 7;
    public float idleTime = 2;
    public Transform[] patrolPoints = new Transform[2];
    public float dieDelayTime = 3;

    void Start()
    {
        ApplyDifficult();
    }
    void ApplyDifficult()
    {
        int ratio = GameManager.instance.SetDifficult();

        maxHealth *= ratio;
        attackDamage *= ratio;
    }


}
