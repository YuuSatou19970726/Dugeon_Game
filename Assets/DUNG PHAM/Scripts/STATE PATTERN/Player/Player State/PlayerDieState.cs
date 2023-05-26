using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : IState
{

    PlayerStateManager player;
    public PlayerDieState(PlayerStateManager player)
    {
        this.player = player;
    }


    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Die");
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
    }


}
