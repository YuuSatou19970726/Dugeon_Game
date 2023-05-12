using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Attack 1");


        player.playerAttack.AttackCast(0);
    }

    public override void ExitState(PlayerStateManager player)
    {


    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.currentState.normalizedTime <= 0.8f) return;

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.attackState1);

        if (player.playerAnimation.currentState.normalizedTime <= 1) return;

        if (!player.inputController.isLeftMousePress)
            player.SwitchState(player.idleState);
    }

}
