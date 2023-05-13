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
        if (enemy.enemyAnimation.CheckState(2) && enemy.enemyAnimation.currentState.normalizedTime < 1) return;

        enemy.SwitchState(enemy.idleState);
    }
}
