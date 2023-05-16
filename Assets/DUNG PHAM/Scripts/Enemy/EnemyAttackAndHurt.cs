using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAndHurt : MonoBehaviour, IDamageable, IAttackable
{
    EnemyController enemyController;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    void Start()
    {
        enemyController.isHurt = false;
    }
    public void GetDamage(float damage)
    {
        enemyController.health -= damage;
        enemyController.isHurt = true;
    }

    public void GetInitDamage(float damage)
    {

    }


}
