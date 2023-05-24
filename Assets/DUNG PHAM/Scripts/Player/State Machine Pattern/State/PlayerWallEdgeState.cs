using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallEdgeState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Wall Grab");

    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.playerController.WallEdgeGrab();
        
        if (player.inputController.inputY > 0)
            player.SwitchState(player.wallClimb);

        if (player.inputController.inputY < 0)
        {
            player.SwitchState(player.fallState);
        }

        if (player.inputController.isJumpPress)
        {
            player.SwitchState(player.wallJumpState);
        }
    }
}
