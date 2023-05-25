using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : IState
{
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Crouch Move");

        player.playerController.EnterCrouch();
    }

    public void ExitState(PlayerStateManager player)
    {
        player.playerController.ExitCrouch();
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            player.playerController.Movement();
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (player.inputController.inputY >= 0)
            player.SwitchState(player.idleState);

        if (player.inputController.inputY < 0 && player.inputController.inputX == 0)
            player.SwitchState(player.crouchState);

        PlayerOneWayPlatform.instance.GetThroughOneWayPlatform();
    }


}
