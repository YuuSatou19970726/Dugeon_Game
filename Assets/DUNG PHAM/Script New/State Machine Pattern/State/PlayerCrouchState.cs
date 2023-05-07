using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
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
        if (player.inputController.inputY >= 0)
            player.SwitchState(player.idleState);

        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            player.SwitchState(player.crouchMoveState);

        PlayerOneWayPlatform.instance.GetThroughOneWayPlatform();
    }


}
