using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IState
{
    PlayerStateManager player;
    Rigidbody2D rigid;


    public PlayerWallJumpState(PlayerStateManager player)
    {
        this.player = player;
        rigid = player.GetComponent<Rigidbody2D>();
    }

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.WALLJUMP);
        player.soundEffect.PlayAudio(2);

        WallJump();

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

        if (rigid.velocity.y < 0)
            player.SwitchState(player.fallState);

        if (player.playerCollision.GroundCheck())
            player.SwitchState(player.idleState);

        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
            player.SwitchState(player.wallLedgeState);
    }

    void WallJump()
    {
        player.playerController.wallTimer = 0;

        GetJumpDirection();

        if (jumpDirection == 0) return;

        rigid.velocity = new Vector2(jumpDirection * player.playerDatabase.moveSpeed, player.playerDatabase.jumpForce);

        player.transform.localScale = new Vector2(jumpDirection, 1);

        player.playerController.StopJump(0.1f);
    }

    [SerializeField] int jumpDirection;

    void GetJumpDirection()
    {
        if (player.playerCollision.isLeftWall)
        {
            jumpDirection = 1;
        }
        else if (player.playerCollision.isRightWall)
        {
            jumpDirection = -1;
        }
    }
}
