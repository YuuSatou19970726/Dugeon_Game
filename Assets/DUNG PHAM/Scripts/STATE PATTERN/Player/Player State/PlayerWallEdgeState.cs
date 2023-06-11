using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallEdgeState : IState
{
    PlayerStateManager player;
    PlayerCollisionDetector playerCollision;

    public PlayerWallEdgeState(PlayerStateManager player)
    {
        this.player = player;
        this.playerCollision = player.playerCollision;
    }
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.LEDGE_GRAB);

    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        WallEdgeGrab();

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.inputController.inputY > 0)
            player.SwitchState(player.wallClimbState);

        if (player.inputController.inputY < 0 || !player.playerCollision.isLeftEdge && !player.playerCollision.isRightEdge)
            player.SwitchState(player.wallSlideState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.wallJumpState);
    }

    void WallEdgeGrab()
    {
        Vector2 position = playerCollision.ledgePoint;
        int side = position.x - player.transform.position.x > 0 ? 1 : -1;

        player.rigid.velocity = Vector2.zero;

        player.transform.localScale = new Vector2(side, 1);

        player.transform.position = new Vector2(position.x - 0.3f * side, position.y - 0.9f);
    }
}
