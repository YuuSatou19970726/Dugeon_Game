using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : IState
{
    string WALLCLB = "Wall Climb";

    PlayerWallLedgeGrabAndClimb playerLedge;
    public PlayerWallClimbState(PlayerWallLedgeGrabAndClimb _playerLedge)
    {
        playerLedge = _playerLedge;
    }

    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(WALLCLB);

        playerLedge.WallClimb();
    }

    public void ExitState(PlayerStateManager player)
    {

    }

    public void FixedUpdateState(PlayerStateManager player)
    {
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.currentState.IsName(WALLCLB)
         && player.playerAnimation.currentState.normalizedTime >= 0.9f)
        {
            player.SwitchState(player.idleState);
        }
    }
}
