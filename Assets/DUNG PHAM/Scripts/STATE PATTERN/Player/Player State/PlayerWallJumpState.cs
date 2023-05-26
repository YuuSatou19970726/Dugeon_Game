using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IState
{
    PlayerStateManager player;
    PlayerWallSlideAndJump playerWall;
    string WALLJUMP = "Wall Jump";


    public PlayerWallJumpState(PlayerStateManager player, PlayerWallSlideAndJump playerWall)
    {
        this.player = player;
        this.playerWall = playerWall;
    }

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(WALLJUMP);

        playerWall.WallJump();

        player.soundEffect.PlayAudio(2);

    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {

        if (player.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            player.SwitchState(player.onAirState);
        }
    }

    public void UpdateState()
    {
    }
}
