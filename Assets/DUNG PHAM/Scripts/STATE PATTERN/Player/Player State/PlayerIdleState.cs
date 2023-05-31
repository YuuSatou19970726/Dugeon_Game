using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    PlayerStateManager player;
    public PlayerIdleState(PlayerStateManager player)
    {
        this.player = player;
    }



    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Idle");

        player.soundEffect.PlayAudio(0);
    }


    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

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

        if (player.playerCollision.isGrounded) return;

        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
            player.SwitchState(player.fallState);

        if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
            player.SwitchState(player.wallSlideState);

        if (player.playerInteract.isClimbing) player.SwitchState(player.ladderState);

    }
}
