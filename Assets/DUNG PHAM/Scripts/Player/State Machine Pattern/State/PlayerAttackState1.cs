using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState1 : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Attack 2");

        player.playerAttack.AttackCast(1);
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
            player.SwitchState(player.attackState2);

        if (player.playerAnimation.currentState.normalizedTime <= 1) return;

        if (!player.inputController.isLeftMousePress)
            player.SwitchState(player.idleState);
    }


}
