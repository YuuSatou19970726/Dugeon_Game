using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Jump");

        player.playerController.Jump();

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
            if (!player.playerDatabase.isLeftWall && !player.playerDatabase.isRightWall)
                player.SwitchState(player.onAirState);

        if (player.playerDatabase.isLeftWall || player.playerDatabase.isRightWall)
            player.SwitchState(player.wallSlideState);

        if (player.playerDatabase.isLeftEdge || player.playerDatabase.isRightEdge)
            player.SwitchState(player.wallEdge);

        if (player.inputController.isLeftMousePress)
            player.SwitchState(player.airAttackState);

        if (player.inputController.isJumpPress)
            player.SwitchState(player.jumpState);

        if (player.inputController.isDashPress)
            player.SwitchState(player.dashState);
    }
}
