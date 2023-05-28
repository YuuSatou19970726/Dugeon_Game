using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IState
{
    PlayerStateManager player;
    PlayerWallSlideAndJump playerWall;
    Rigidbody2D playerRigid;
    string WALLJUMP = "Wall Jump";


    public PlayerWallJumpState(PlayerStateManager player, PlayerWallSlideAndJump playerWall)
    {
        this.player = player;
        this.playerWall = playerWall;
    }

    public void EnterState()
    {
        GetObjectComponents();

        player.playerAnimation.PlayAnimatorClip(WALLJUMP);

        playerWall.WallJump();

        player.soundEffect.PlayAudio(2);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        if (playerRigid.velocity.y < 0)
        {
            player.SwitchState(player.onAirState);
        }
    }

    void GetObjectComponents()
    {
        playerRigid = player.GetComponent<Rigidbody2D>();
    }
}
