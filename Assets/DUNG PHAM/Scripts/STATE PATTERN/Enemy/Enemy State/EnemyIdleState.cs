using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    EnemyStateMachine enemy;
    float timer = 0f;

    public EnemyIdleState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.enemyAnimation.PlayAnimation(0);
    }

    public void ExitState()
    {
        timer = 0;
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        if (enemy.enemyController.isHurt)
            enemy.SwitchState(enemy.hurtState);

        if (enemy.enemyController.playerDetected && !enemy.enemyController.playerDied)
        {
            enemy.SwitchState(enemy.chaseState);
        }

        if (timer < enemy.enemyController.idleTime)
        {
            timer += Time.deltaTime;
            return;
        }


        if (!enemy.enemyController.playerDetected)
        {
            enemy.SwitchState(enemy.patrolState);
        }
    }


}
