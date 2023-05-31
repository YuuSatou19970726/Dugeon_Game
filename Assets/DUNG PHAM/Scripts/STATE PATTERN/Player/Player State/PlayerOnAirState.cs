using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnAirState : IState
{

    PlayerStateManager player;
    public PlayerOnAirState(PlayerStateManager player)
    {
        this.player = player;
    }


    Rigidbody2D rigid;

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Air Roll");

        rigid = player.GetComponent<Rigidbody2D>();
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

        if (player.GetComponent<Rigidbody2D>().velocity.y < -2)
            if (player.playerAnimation.currentState.normalizedTime > 0.5f)
                player.SwitchState(player.fallState);

        if (player.playerCollision.isGrounded)
            player.SwitchState(player.idleState);

        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
            player.SwitchState(player.wallEdge);

        if (player.playerCollision.isLeftWall || player.playerCollision.isRightWall)
            player.SwitchState(player.wallSlideState);

        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);
    }
}
