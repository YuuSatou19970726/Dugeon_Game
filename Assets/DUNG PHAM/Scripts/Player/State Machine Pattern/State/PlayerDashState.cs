using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : IState
{
    string DASH = "Wall Jump";

    PlayerDash playerDash;

    public PlayerDashState(PlayerDash _playerDash)
    {
        playerDash = _playerDash;
    }

    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip(DASH);
        playerDash.Dash();
    }

    public void ExitState(PlayerStateManager player)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void FixedUpdateState(PlayerStateManager player)
    {

    }

    public void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.CheckCurrentClip(DASH)
        && player.playerAnimation.CurrentClipNormalize() > player.playerDatabase.dashingTime)
        {
            player.SwitchState(player.idleState);
        }
    }


}
