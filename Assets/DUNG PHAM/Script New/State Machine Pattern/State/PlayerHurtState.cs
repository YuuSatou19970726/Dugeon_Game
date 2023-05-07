using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Hurt");
    }

    public override void ExitState(PlayerStateManager player)
    {
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.currentState.IsName("Hurt") && player.playerAnimation.currentState.normalizedTime > 1)
        {
            player.SwitchState(player.idleState);
            player.playerMovementController.playerDatabase.isHurt = false;
        }
    }


}
