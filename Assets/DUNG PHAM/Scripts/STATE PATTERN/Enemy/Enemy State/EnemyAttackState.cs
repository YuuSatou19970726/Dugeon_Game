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

        if (enemy.enemyAnimation.CheckState(2) && enemy.enemyAnimation.currentState.normalizedTime > 1)
            enemy.SwitchState(enemy.idleState);
    }
}
