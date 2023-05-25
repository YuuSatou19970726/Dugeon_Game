using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IState
{
    string WALLJUMP = "Wall Jump";

    PlayerWallSlideAndJump playerWall;
    public PlayerWallJumpState(PlayerWallSlideAndJump _playerWall)
    {
        playerWall = _playerWall;
    }

    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(WALLJUMP);

        playerWall.WallJump();

        player.soundEffect.PlayAudio(2);

    }

    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {

        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            player.SwitchState(player.onAirState);
        }
    }

    public void UpdateState(PlayerStateManager player)
    {
    }
}
