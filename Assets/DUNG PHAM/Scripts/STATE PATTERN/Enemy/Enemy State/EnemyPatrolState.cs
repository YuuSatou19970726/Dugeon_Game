using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{
    EnemyStateMachine enemy;

    public EnemyPatrolState(EnemyStateMachine enemy)
    {
        this.enemy = enemy;
    }

    public void EnterState()
    {
        enemy.enemyAnimation.PlayAnimation(1);
    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        enemy.enemyController.MoveToTarget(enemy.enemyController.target.position);

        if (Vector2.Distance(enemy.transform.position, enemy.enemyController.target.position) < 0.5f) enemy.SwitchState(enemy.idleState);

        if (enemy.enemyController.playerDetected) enemy.SwitchState(enemy.chaseState);
    }


}
