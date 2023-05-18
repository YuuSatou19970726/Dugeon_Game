using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Wall Jump");

        player.playerMovementController.WallJump();

        player.soundEffect.PlayAudio(2);

    }

    public override void ExitState(PlayerStateManager player)
    {
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {

        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            player.SwitchState(player.onAirState);
        }
    }

    public override void UpdateState(PlayerStateManager player)
    {
    }
}
