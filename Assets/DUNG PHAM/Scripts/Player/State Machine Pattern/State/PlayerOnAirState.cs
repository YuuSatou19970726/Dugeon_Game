using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnAirState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Air Roll");
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.MoveOnAir();

        if (player.GetComponent<Rigidbody2D>().velocity.y < -2)
            if (player.playerAnimation.currentState.normalizedTime > 0.5f)
                player.SwitchState(player.fallState);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerDatabase.isGrounded)
        {
            player.SwitchState(player.idleState);
        }

        if (player.playerDatabase.isLeftWall || player.playerDatabase.isRightWall)
        {
            if (player.playerDatabase.isLeftEdge || player.playerDatabase.isRightEdge)
                player.SwitchState(player.wallEdge);

            player.SwitchState(player.wallSlideState);
        }

        if (player.inputController.isDashPress)
        {
            player.SwitchState(player.dashState);
        }

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);
    }
}
