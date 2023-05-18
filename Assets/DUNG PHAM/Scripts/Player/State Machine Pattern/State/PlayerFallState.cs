using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Fall");
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerMovementController.MoveOnAir();

        if (player.playerMovementController.isGrounded)
        {
            // if (player.GetComponent<Rigidbody2D>().velocity.y > -player.playerMovementController.playerDatabase.maxFallVelocity / 2)
            // {
            //     player.SwitchState(player.idleState);
            // }
            // else
            // {
                player.SwitchState(player.crouchState);
                player.soundEffect.PlayAudio(3);
            // }
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerMovementController.isGrounded) return;

        if (player.playerMovementController.isLeftWall || player.playerMovementController.isRightWall)
        {
            player.SwitchState(player.wallSlideState);
        }


        if (player.inputController.isLeftMousePress)
        {
            player.SwitchState(player.airAttackState);
        }
    }
}
