using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState2 : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Attack 3");

        player.playerAttack.AttackCast(2);
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.Movement();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.currentState.normalizedTime <= 0.5f) return;

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);

        if (player.playerAnimation.currentState.normalizedTime <= 1f) return;

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.attackState);

        if (player.playerAnimation.currentState.normalizedTime <= 1.5f) return;

        if (!player.inputController.isLeftMousePress)
            player.SwitchState(player.idleState);
    }


}
