using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : IState
{
    EnemyStateMachine enemy;

    public EnemyHurtState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.enemyAnimation.PlayAnimation(3);
    }

    public void ExitState()
    {
        enemy.enemyController.isHurt = false;
    }

    public void FixedUpdateState()
    {
    }


    public void UpdateState()
    {
        if (enemy.enemyController.health <= 0) enemy.SwitchState(enemy.dieState);

        if (enemy.enemyAnimation.CheckState(3) && enemy.enemyAnimation.currentState.normalizedTime > 1)
            enemy.SwitchState(enemy.idleState);
    }
}
