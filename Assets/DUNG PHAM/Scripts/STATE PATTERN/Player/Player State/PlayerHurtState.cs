using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : IState
{
    PlayerStateManager player;
    public PlayerHurtState(PlayerStateManager player)
    {
        this.player = player;
    }


    string HURT = "Hurt";
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(HURT);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.Movement();
    }

    public void UpdateState()
    {
        if (player.playerAnimation.currentState.IsName(HURT) && player.playerAnimation.currentState.normalizedTime > 1)
        {
            player.SwitchState(player.idleState);
            player.playerDatabase.isHurt = false;
        }
    }


}
