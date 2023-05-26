using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallEdgeState : IState
{
    PlayerStateManager player;
    PlayerWallLedgeGrabAndClimb playerLedge;
    string GRAB = "Wall Grab";


    public PlayerWallEdgeState(PlayerStateManager player, PlayerWallLedgeGrabAndClimb playerLedge)
    {
        this.player = player;
        this.playerLedge = playerLedge;
    }
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(GRAB);

    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        playerLedge.WallEdgeGrab();

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
