using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : IState
{

    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Wall Jump");

        player.playerController.WallJump();

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
