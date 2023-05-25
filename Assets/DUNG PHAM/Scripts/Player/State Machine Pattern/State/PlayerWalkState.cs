using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : IState
{
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Walk");
    }

    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
        player.playerController.Movement();
    }

    public void UpdateState(PlayerStateManager player)
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
