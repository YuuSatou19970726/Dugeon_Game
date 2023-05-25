using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : IState
{
    string WALLCLB = "Wall Climb";

    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(WALLCLB);

        player.playerController.WallClimb();
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
