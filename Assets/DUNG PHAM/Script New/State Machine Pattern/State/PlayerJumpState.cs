using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Jump");

        player.playerMovementController.Jump();
    }


    public override void ExitState(PlayerStateManager player)
    {
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerMovementController.MoveOnAir();

        if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y) < 2)
            if (!player.playerMovementController.isLeftWall && !player.playerMovementController.isRightWall)
                player.SwitchState(player.onAirState);
                
        if (player.playerMovementController.isLeftWall || player.playerMovementController.isRightWall)
            player.SwitchState(player.wallSlideState);
    }

    public override void UpdateState(PlayerStateManager player)
    {


        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.airAttackState);



        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);
    }
}
