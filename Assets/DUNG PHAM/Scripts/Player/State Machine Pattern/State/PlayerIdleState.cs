using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Idle");

        player.soundEffect.PlayAudio(0);
    }



    public override void ExitState(PlayerStateManager player)
    {
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        if (player.playerMovementController.isGrounded) return;

        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
            player.SwitchState(player.fallState);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.playerMovementController.isGrounded) return;

        if (player.inputController.inputX != 0)
        {
            player.SwitchState(player.walkState);
        }

        if (player.inputController.isJumpPress)
        {
            player.SwitchState(player.jumpState);
        }

        if (player.inputController.inputY < 0)
        {
            player.SwitchState(player.crouchState);
        }

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.attackState);

        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);

        if (player.playerMovementController.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerMovementController.playerDatabase.isDied)
            player.SwitchState(player.dieState);
    }
}
