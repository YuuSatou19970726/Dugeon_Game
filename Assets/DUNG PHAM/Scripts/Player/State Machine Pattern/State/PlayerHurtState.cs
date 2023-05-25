using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : IState
{
    string HURT = "Hurt";
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(HURT);
    }

    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.Movement();
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.currentState.IsName(HURT) && player.playerAnimation.currentState.normalizedTime > 1)
        {
            player.SwitchState(player.idleState);
            player.playerDatabase.isHurt = false;
        }
    }


}
