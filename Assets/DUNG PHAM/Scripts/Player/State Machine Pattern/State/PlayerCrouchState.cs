using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    // float timer = 0;
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Crouch");

        player.playerMovementController.EnterCrouch();
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.playerMovementController.ExitCrouch();
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
    }

    public override void UpdateState(PlayerStateManager player)
    {
        // timer += Time.deltaTime;

        if (player.inputController.inputY >= 0)
        {
            // if (player.inputController.inputX != 0) timer = 1f;

            // if (timer < 0.5f) return;

            player.SwitchState(player.idleState);
            // timer = 0;
        }

        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            player.SwitchState(player.crouchMoveState);

        PlayerOneWayPlatform.instance.GetThroughOneWayPlatform();
    }


}
