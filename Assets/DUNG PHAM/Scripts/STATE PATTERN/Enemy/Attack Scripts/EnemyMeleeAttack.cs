using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IAttacker
{
    EnemyController enemyController;
    EnemyDatabase enemyDatabase;
    [SerializeField] Collider2D[] attackColliders;
    Collider2D enemyColi;
    List<Collider2D> hits = new List<Collider2D>();

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        enemyDatabase = GetComponent<EnemyDatabase>();
        enemyColi = GetComponent<Collider2D>();

    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        for (int x = 0; x < attackColliders.Length; x++)
        {
            StartCoroutine(AttackLaunch(x));

            yield return new WaitForSeconds(enemyDatabase.timePerAttack);
        }
    }

    IEnumerator AttackLaunch(int coliIndex)
    {
        //Delay to fix with animation
        yield return new WaitForSeconds(enemyDatabase.attackDelay);

        int count = Physics2D.OverlapCollider(attackColliders[coliIndex], enemyDatabase.targetFilter, hits);

        foreach (Collider2D hitColi in hits)
        {
            if (hitColi == enemyColi) continue;

            if (hitColi.GetComponent<IDamageable>() != null)
                hitColi.GetComponent<IDamageable>().GetDamage(enemyDatabase.attackDamage, transform);

            if (hitColi.GetComponent<IStopAttack>() != null)
                enemyController.playerDied = hitColi.GetComponent<IStopAttack>().StopAttack();
        }

        enemyController.RunAttackCooldownCoroutine();
    }

}
