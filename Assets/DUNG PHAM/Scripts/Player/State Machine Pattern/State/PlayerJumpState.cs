using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    PlayerJump playerJump;

    public PlayerJumpState(PlayerJump _playerJump)
    {
        playerJump = _playerJump;
    }


    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Jump");

        playerJump.Jump();

        player.soundEffect.PlayAudio(2);
    }


    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.MoveOnAir();
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y) < 2)
            if (!player.playerCollision.isLeftWall && !player.playerCollision.isRightWall)
                player.SwitchState(player.onAirState);

        if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
            player.SwitchState(player.wallSlideState);

        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
            player.SwitchState(player.wallEdge);

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.airAttackState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);

        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);
    }
}
