using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnAirState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Air Roll");
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerMovementController.MoveOnAir();

        if (player.playerMovementController.isGrounded)
        {
            player.SwitchState(player.idleState);
        }
        else
        {
            if (player.playerMovementController.isLeftWall || player.playerMovementController.isRightWall)
            {
                player.SwitchState(player.wallSlideState);
            }
        }

        if (player.GetComponent<Rigidbody2D>().velocity.y < -2)
            if (player.playerAnimation.currentState.normalizedTime > 0.5f)
                player.SwitchState(player.fallState);
    }

    public override void UpdateState(PlayerStateManager player)
    {


    }




}
