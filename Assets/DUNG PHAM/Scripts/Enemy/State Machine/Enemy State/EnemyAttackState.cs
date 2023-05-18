using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.enemyAnimation.PlayAnimation(2);
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
    }

    public override void FixedUpdateState(EnemyStateMachine enemy)
    {

    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (!enemy.enemyAnimation.CheckState(2)) return;

        if (enemy.enemyAnimation.currentState.normalizedTime > 0.8f)
        {
            enemy.enemyController.Attack();
        }

        if (enemy.enemyAnimation.currentState.normalizedTime > 1)
            enemy.SwitchState(enemy.idleState);
    }
}
