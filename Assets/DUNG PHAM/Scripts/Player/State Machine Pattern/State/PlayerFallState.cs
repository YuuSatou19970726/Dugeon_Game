using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Fall");

        player.soundEffect.PlayAudio(0);
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerMovementController.MoveOnAir();

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerMovementController.isGrounded)
        {
            player.SwitchState(player.crouchState);
            player.soundEffect.PlayAudio(3);
        }

        // if (player.playerMovementController.isRightEdge || player.playerMovementController.isLeftEdge)
        //     player.SwitchState(player.wallEdge);

        if (player.playerMovementController.isLeftWall || player.playerMovementController.isRightWall)
        {
            player.SwitchState(player.wallSlideState);
        }


        if (player.inputController.isLeftMousePress)
        {
            player.SwitchState(player.airAttackState);
        }

        if (player.inputController.isDashPress)
        {
            player.SwitchState(player.dashState);
        }

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);
    }
}
