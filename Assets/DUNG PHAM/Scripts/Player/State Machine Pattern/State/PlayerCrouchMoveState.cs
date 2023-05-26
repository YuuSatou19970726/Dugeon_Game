using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Crouch Move");

        player.playerController.EnterCrouch();
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.playerController.ExitCrouch();
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            player.playerController.Movement();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.inputController.inputY >= 0)
            player.SwitchState(player.idleState);

        if (player.inputController.inputY < 0 && player.inputController.inputX == 0)
            player.SwitchState(player.crouchState);

        PlayerOneWayPlatform.instance.GetThroughOneWayPlatform();
    }


}
