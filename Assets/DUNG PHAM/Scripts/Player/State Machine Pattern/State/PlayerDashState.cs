using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Wall Jump");
        player.playerMovementController.Dash();
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {

    }

    public override void UpdateState(PlayerStateManager player)
    {

        if (player.playerAnimation.currentState.IsName("Wall Jump")
        && player.playerAnimation.currentState.normalizedTime > player.playerMovementController.dashingTime)
        {
            player.SwitchState(player.idleState);
        }
    }


}
