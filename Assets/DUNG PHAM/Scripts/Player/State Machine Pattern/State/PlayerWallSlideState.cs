using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : IState
{
    string WALLSLIDE = "Wall Slide";
    PlayerWallSlideAndJump playerWall;
    PlayerWallLedgeGrabAndClimb playerLedge;
    public PlayerWallSlideState(PlayerWallSlideAndJump playerWall)
    {
        this.playerWall = playerWall;
    }
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(WALLSLIDE);

        player.soundEffect.PlayAudio(0);
    }

    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.MoveOnAir();
        playerWall.WallSlide();
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (player.inputController.isJumpPress)
            player.SwitchState(player.wallJumpState);

        if (player.playerCollision.isGrounded)
            player.SwitchState(player.idleState);

        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
        {
            player.SwitchState(player.wallEdge);
        }

        if (!player.playerCollision.isLeftWall && !player.playerCollision.isRightWall)
            player.SwitchState(player.fallState);
    }
}
