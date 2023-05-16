using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    float timer = 0f;
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.enemyAnimation.PlayAnimation(0);
    }

    public override void ExitState(EnemyStateMachine enemy)
    {

    }

    public override void FixedUpdateState(EnemyStateMachine enemy)
    {

    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.enemyController.playerDetected) enemy.SwitchState(enemy.chaseState);

        if (timer < enemy.enemyController.enemyProperties.idleTime)
        {
            timer += Time.deltaTime;
            return;
        }

        if (!enemy.enemyController.playerDetected)
        {
            enemy.SwitchState(enemy.patrolState);
            enemy.enemyController.isResting = false;
            timer = 0;
        }
    }


}
