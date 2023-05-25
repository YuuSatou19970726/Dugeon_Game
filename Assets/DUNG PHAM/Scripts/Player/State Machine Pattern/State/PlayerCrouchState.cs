using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : IState
{
    float timer = 0;
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Crouch");

        player.playerController.EnterCrouch();
    }

    public void ExitState(PlayerStateManager player)
    {
        player.playerController.ExitCrouch();
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
    }

    public void UpdateState(PlayerStateManager player)
    {
        timer += Time.deltaTime;

        if (player.inputController.inputY >= 0)
        {
            if (player.inputController.inputX != 0) timer = 1f;

            if (timer < 0.5f) return;

            player.SwitchState(player.idleState);
            timer = 0;
        }

        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            player.SwitchState(player.crouchMoveState);

        PlayerOneWayPlatform.instance.GetThroughOneWayPlatform();
    }


}
