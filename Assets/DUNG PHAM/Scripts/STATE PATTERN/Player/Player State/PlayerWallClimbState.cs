using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : IState
{

    PlayerStateManager player;
    PlayerWallLedgeGrabAndClimb playerLedge;
    string WALLCLB = "Wall Climb";


    public PlayerWallClimbState(PlayerStateManager player, PlayerWallLedgeGrabAndClimb playerLedge)
    {
        this.player = player;
        this.playerLedge = playerLedge;
    }



    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(WALLCLB);

        playerLedge.WallClimb();
    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerAnimation.currentState.IsName(WALLCLB)
         && player.playerAnimation.currentState.normalizedTime >= 0.9f)
        {
            player.SwitchState(player.idleState);
        }
    }
}
