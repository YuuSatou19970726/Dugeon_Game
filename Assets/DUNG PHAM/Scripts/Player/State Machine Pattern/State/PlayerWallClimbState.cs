using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Wall Climb");

        player.playerController.WallClimb();
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.currentState.IsName("Wall Climb")
         && player.playerAnimation.currentState.normalizedTime >= 0.9f)
        {
            player.SwitchState(player.idleState);
        }
    }
}
