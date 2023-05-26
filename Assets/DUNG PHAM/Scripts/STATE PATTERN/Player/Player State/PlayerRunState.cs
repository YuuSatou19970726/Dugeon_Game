using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{

    PlayerStateManager player;
    public PlayerRunState(PlayerStateManager player)
    {
        this.player = player;
    }



    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Run");
        player.soundEffect.PlayAudio(1);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.Movement();
    }

    public void UpdateState()
    {
        if (player.playerCollision.isGrounded)

        {
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

        if (!player.playerCollision.isGrounded)
        {
            player.SwitchState(player.fallState);

            if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
                player.SwitchState(player.wallSlideState);
        }
    }

}
