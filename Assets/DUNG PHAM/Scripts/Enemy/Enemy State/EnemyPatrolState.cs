using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.enemyAnimation.PlayAnimation(1);
    }

    public override void ExitState(EnemyStateMachine enemy)
    {

    }

    public override void FixedUpdateState(EnemyStateMachine enemy)
    {
        enemy.enemyController.MoveToTarget(enemy.enemyController.target.position);
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.enemyController.isResting) enemy.SwitchState(enemy.idleState);

        if (enemy.enemyController.playerDetected) enemy.SwitchState(enemy.chaseState);
    }


}
