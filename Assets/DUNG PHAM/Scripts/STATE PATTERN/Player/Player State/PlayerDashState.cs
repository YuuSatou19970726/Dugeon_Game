using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : IState
{

    PlayerStateManager player;
    PlayerDash playerDash;

    public PlayerDashState(PlayerStateManager player, PlayerDash playerDash)
    {
        this.player = player;
        this.playerDash = playerDash;

    }


    string DASH = "Wall Jump";


    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(DASH);
        playerDash.Dash();
    }

    public void ExitState()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

        if (player.playerAnimation.CheckCurrentClip(DASH)
        && player.playerAnimation.CurrentClipNormalize() > player.playerDatabase.dashingTime)
        {
            player.SwitchState(player.idleState);
        }
    }


}
