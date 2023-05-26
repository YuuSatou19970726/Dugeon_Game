using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IState
{
    EnemyStateMachine enemy;

    public EnemyChaseState(EnemyStateMachine enemy)
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
        if (enemy.enemyController.playerColi)
            enemy.enemyController.MoveToTarget(enemy.enemyController.playerColi.transform.position);

        if (enemy.enemyController.playerInRange && enemy.enemyController.canAttack)
            enemy.SwitchState(enemy.attackState);

        if (!enemy.enemyController.playerDetected)
            enemy.SwitchState(enemy.idleState);

        if (enemy.enemyController.isHurt)
            enemy.SwitchState(enemy.hurtState);
    }


}
