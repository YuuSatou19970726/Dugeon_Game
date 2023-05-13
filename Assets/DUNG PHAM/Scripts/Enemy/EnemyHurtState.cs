using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
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
        if (enemy.enemyAnimation.CheckState(0) && enemy.enemyAnimation.currentState.normalizedTime < 1) return;

        enemy.SwitchState(enemy.idleState);
    }
}
