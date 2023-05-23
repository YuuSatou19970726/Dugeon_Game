using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Wall Slide");

        player.soundEffect.PlayAudio(0);
    }

    public override void ExitState(PlayerStateManager player)
    {
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerMovementController.MoveOnAir();
        player.playerMovementController.WallSlide();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.inputController.isJumpPress)
        {
            player.SwitchState(player.wallJumpState);
        }

        if (player.playerMovementController.isGrounded)
            player.SwitchState(player.idleState);

        if (player.playerMovementController.isRightEdge || player.playerMovementController.isLeftEdge)
            player.SwitchState(player.wallEdge);

        if (!player.playerMovementController.isLeftWall && !player.playerMovementController.isRightWall)
            player.SwitchState(player.fallState);
    }
}
