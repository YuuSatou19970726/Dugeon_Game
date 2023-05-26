using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState
{
    PlayerStateManager player;
    public PlayerWalkState(PlayerStateManager player)
    {
        this.player = player;
    }


    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Walk");
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.Movement();
    }

    public void UpdateState()
    {

        if (player.inputController.inputX != 0)
            if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > 5)
                player.SwitchState(player.runState);


        if (player.inputController.inputX == 0)
            player.SwitchState(player.idleState);

        if (player.inputController.isJumpPress)
        {
            player.SwitchState(player.jumpState);
        }
    }

}
