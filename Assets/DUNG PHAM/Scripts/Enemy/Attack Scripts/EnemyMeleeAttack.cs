using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IAttacker
{
    EnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void Attack(float damage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyController.maxAttackRange, enemyController.attackLayer);

        foreach (Collider2D hitColi in hits)
        {
            if (hitColi.GetComponent<IDamageable>() == null) continue;

            hitColi.GetComponent<IDamageable>().GetDamage(damage);

            if (hitColi.GetComponent<IStopAttack>() != null)
                enemyController.playerDied = hitColi.GetComponent<IStopAttack>().StopAttack();
        }

    }
}
