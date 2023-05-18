using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.enemyAnimation.PlayAnimation(3);
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        enemy.enemyController.isHurt = false;
    }

    public override void FixedUpdateState(EnemyStateMachine enemy)
    {
    }


    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.enemyController.health <= 0) enemy.SwitchState(enemy.dieState);

        if (enemy.enemyAnimation.CheckState(3) && enemy.enemyAnimation.currentState.normalizedTime > 1)
            enemy.SwitchState(enemy.idleState);
    }
}
