using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : IState
{
    PlayerStateManager player;
    Rigidbody2D rigid;

    public PlayerWallSlideState(PlayerStateManager player)
    {
        this.player = player;
        rigid = player.GetComponent<Rigidbody2D>();
    }

    /**************************************************************************************************************************************************/

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.WALLSLIDE);

        player.soundEffect.PlayAudio(0);
        player.playerController.UnGravity(0.05f);
    }

    /**************************************************************************************************************************************************/

    public void ExitState()
    {
    }

    /**************************************************************************************************************************************************/

    public void FixedUpdateState()
    {
        player.playerController.MoveOnAir();
        WallSlide();
    }

    /**************************************************************************************************************************************************/

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.wallJumpState);

        if (player.playerCollision.GroundCheck())
            player.SwitchState(player.idleState);

        if (player.playerCollision.isLeftEdge || player.playerCollision.isRightEdge)
            player.SwitchState(player.wallLedgeState);

        if (!player.playerCollision.isLeftWall && !player.playerCollision.isRightWall)
            player.SwitchState(player.fallState);
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

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

    /**************************************************************************************************************************************************/

    public void WallSlide()
    {
        GetJumpDirection();

        if (jumpDirection != 0)
            player.transform.localScale = new Vector2(-jumpDirection, 1);
    }

    /**************************************************************************************************************************************************/



}
