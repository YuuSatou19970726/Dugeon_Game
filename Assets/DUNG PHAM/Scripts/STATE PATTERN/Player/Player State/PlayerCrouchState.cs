using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : IState
{

    PlayerStateManager player;
    public PlayerCrouchState(PlayerStateManager player)
    {
        this.player = player;
    }

    float timer = 0;
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Crouch");

        player.playerController.EnterCrouch();
    }

    public void ExitState()
    {
        player.playerController.ExitCrouch();
    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        timer += Time.deltaTime;

        if (player.inputController.inputY >= 0)
        {
            if (player.inputController.inputX != 0) timer = 1f;

            if (timer < 0.5f) return;

            timer = 0;
            player.SwitchState(player.idleState);
        }

        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            player.SwitchState(player.crouchMoveState);

        PlayerOneWayPlatform.instance.GetThroughOneWayPlatform();
    }


}
