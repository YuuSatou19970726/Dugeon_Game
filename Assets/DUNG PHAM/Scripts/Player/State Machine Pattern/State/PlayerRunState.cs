using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Run");
        player.soundEffect.PlayAudio(1);
    }



    public override void ExitState(PlayerStateManager player)
    {
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.Movement();

        if (player.playerDatabase.isGrounded) return;

        player.SwitchState(player.fallState);

        if (player.playerDatabase.isLeftWall || player.playerDatabase.isRightWall)
            player.SwitchState(player.wallSlideState);

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (!player.playerDatabase.isGrounded) return;

        if (player.inputController.inputX == 0)
        {
            if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) < 5)
                player.SwitchState(player.walkState);
        }

        if (player.inputController.isJumpPress)
        {
            player.SwitchState(player.jumpState);
        }

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.attackState);

        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
        {
            player.SwitchState(player.crouchMoveState);
        }

        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);
    }

}
