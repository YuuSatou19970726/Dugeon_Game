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
        player.playerMovementController.onWall = true;
        player.playerMovementController.MoveOnAir();

        if (player.playerMovementController.isGrounded && player.GetComponent<Rigidbody2D>().velocity.y == 0)
            player.SwitchState(player.idleState);

        if (!player.playerMovementController.isGrounded
        && player.inputController.inputX != 0
        && !player.playerMovementController.isLeftWall
        && !player.playerMovementController.isRightWall)
            player.SwitchState(player.fallState);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.inputController.isJumpPress)
        {
            player.SwitchState(player.wallJumpState);
        }

    }


}
