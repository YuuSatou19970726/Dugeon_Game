using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    string DASH = "Wall Jump";
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(DASH);
        player.playerController.Dash();
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {

    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.CheckCurrentClip(DASH)
        && player.playerAnimation.CurrentClipNormalize() > player.playerDatabase.dashingTime)
        {
            player.SwitchState(player.idleState);
        }
    }


}
