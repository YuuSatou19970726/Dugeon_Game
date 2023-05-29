using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : IState
{
    EnemyStateMachine enemy;

    public EnemyDieState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.enemyAnimation.PlayAnimation(4);
        enemy.enemyController.DieTimeDelay();
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
    }
}
