using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.enemyAnimation.PlayAnimation(4);
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
    }

    public override void FixedUpdateState(EnemyStateMachine enemy)
    {
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
    }



}
