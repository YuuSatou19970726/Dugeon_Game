using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    EnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    void Start()
    {
        enemyController.canAttack = true;
        enemyController.enemyProperties.isAttacking = false;
    }
    void Update()
    {
        if (enemyController.enemyProperties.isDead) return;
        
        MeleeAttack();
    }

    void MeleeAttack()
    {
        if (!enemyController.enemyProperties.isAttacking) return;

        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemyController.enemyProperties.maxAttackRange, enemyController.enemyProperties.AttackLayer);

        if (hit)
        {
            // if(enemyController.enemyProperties.faceDirection>0 hit.transform.position.x)
            hit.GetComponent<IDamageable>().GetDamage(enemyController.enemyProperties.attackDamage);
        }
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        enemyController.canAttack = false;
        enemyController.enemyProperties.isAttacking = false;
        yield return new WaitForSeconds(enemyController.enemyProperties.attackSpeed);

        enemyController.canAttack = true;
    }
}
