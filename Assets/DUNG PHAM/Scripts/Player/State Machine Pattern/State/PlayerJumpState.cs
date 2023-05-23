using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Jump");

        player.playerMovementController.Jump();

        player.soundEffect.PlayAudio(2);
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

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerMovementController.isLeftWall || player.playerMovementController.isRightWall)
            player.SwitchState(player.wallSlideState);


        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.airAttackState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);

        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);
    }
}
