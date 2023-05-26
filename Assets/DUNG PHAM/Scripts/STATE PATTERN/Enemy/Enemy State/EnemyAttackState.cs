using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    EnemyStateMachine enemy;

    public EnemyAttackState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }


    public void EnterState()
    {
        enemy.enemyAnimation.PlayAnimation(2);

        enemy.enemyController.Attack();
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {

    }
    public void UpdateState()
    {
        if (enemy.enemyAnimation.CheckState(2))
        {
            if (enemy.enemyAnimation.currentState.normalizedTime > 0.5f && enemy.enemyController.isHurt)
                enemy.SwitchState(enemy.hurtState);

            if (enemy.enemyAnimation.currentState.normalizedTime > 1)
                enemy.SwitchState(enemy.idleState);
        }
    }
}
