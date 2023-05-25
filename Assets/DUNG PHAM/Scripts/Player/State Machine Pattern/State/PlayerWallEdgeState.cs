using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallEdgeState : IState
{
    string GRAB = "Wall Grab";
    PlayerWallLedgeGrabAndClimb playerLedge;
    public PlayerWallEdgeState(PlayerWallLedgeGrabAndClimb _playerLedge)
    {
        playerLedge = _playerLedge;
    }
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(GRAB);

    }

    public void ExitState(PlayerStateManager player)
    {

    }

    public void FixedUpdateState(PlayerStateManager player)
    {
    }

    public void UpdateState(PlayerStateManager player)
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
