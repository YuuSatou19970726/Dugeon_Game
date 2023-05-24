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
        player.playerController.MoveOnAir();
        player.playerController.WallSlide();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.inputController.isJumpPress)
            player.SwitchState(player.wallJumpState);

        if (player.playerDatabase.isGrounded)
            player.SwitchState(player.idleState);

        if (player.playerDatabase.isLeftEdge || player.playerDatabase.isRightEdge)
        {            
            player.SwitchState(player.wallEdge);
        }

        if (!player.playerDatabase.isLeftWall && !player.playerDatabase.isRightWall)
            player.SwitchState(player.fallState);
    }
}
