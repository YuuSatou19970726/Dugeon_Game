using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState, IDamageable
{
    EnemyController enemyController;
    float health;
    float healthRate;
    public GameObject healthBar;
    public GameObject healthBarHolder;

    void Start()
    {
        health = enemyController.enemyProperties.maxHealth;
    }
    void Update()
    {
        DisplayHealth();

        if (health <= 0) { StartCoroutine(ObjectDie()); return; }

        if (Input.GetKeyDown(KeyCode.T)) GetDamage(10);
    }


    void DisplayHealth()
    {
        healthRate = health / enemyController.enemyProperties.maxHealth;

        healthBar.transform.localScale = new Vector3(healthRate, healthBar.transform.localScale.y, 0f);

        healthBarHolder.transform.localScale = new Vector3(enemyController.enemyProperties.faceDirection, 1, 1);
    }


    public void GetDamage(float damage)
    {

        if (health <= 0) health = 0;

        else health -= damage;

        enemyController.enemyProperties.isHurting = true;
    }


    IEnumerator ObjectDie()
    {
        enemyController.enemyProperties.isDead = true;

        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public override void EnterState(EnemyStateMachine enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void FixedUpdateState(EnemyStateMachine enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        throw new System.NotImplementedException();
    }
}
