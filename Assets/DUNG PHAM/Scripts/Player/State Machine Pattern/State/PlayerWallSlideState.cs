using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : IState
{
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Wall Slide");

        player.soundEffect.PlayAudio(0);
    }

    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.MoveOnAir();
        player.playerController.WallSlide();
    }

    public void UpdateState(PlayerStateManager player)
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
