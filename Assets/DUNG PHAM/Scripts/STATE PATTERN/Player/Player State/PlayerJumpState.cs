using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    PlayerStateManager player;
    public PlayerJumpState(PlayerStateManager player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        Jump();

        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.JUMP);
        player.soundEffect.PlayAudio(2);

    }


    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.MoveOnAir();
    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (Mathf.Abs(player.rigid.velocity.y) < 2)
            if (!player.playerCollision.isLeftWall && !player.playerCollision.isRightWall)
                player.SwitchState(player.onAirState);

        if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
            player.SwitchState(player.wallSlideState);

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.airAttackState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);

        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);

        if (player.playerInteract.isClimbing)
            player.SwitchState(player.ladderState);



        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
            player.SwitchState(player.wallLedgeState);
    }

    void Jump()
    {
        if (player.playerController.jumpCount <= 0) return;
        player.playerController.jumpTimer = 0;

        player.rigid.velocity = new Vector2(player.rigid.velocity.x, player.playerDatabase.jumpForce);

        player.playerController.jumpCount--;
    }
}