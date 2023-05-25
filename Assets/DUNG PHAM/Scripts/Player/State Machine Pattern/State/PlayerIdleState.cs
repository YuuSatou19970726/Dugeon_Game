using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Idle");

        player.soundEffect.PlayAudio(0);
    }


    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {

    }

    public void UpdateState(PlayerStateManager player)
    {
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

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerDatabase.isGrounded) return;

        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
            player.SwitchState(player.fallState);

        if (player.playerDatabase.isLeftWall || player.playerDatabase.isRightWall)
            player.SwitchState(player.wallSlideState);
    }
}
