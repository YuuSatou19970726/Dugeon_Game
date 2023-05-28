using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : IState
{

    PlayerStateManager player;
    public PlayerCrouchMoveState(PlayerStateManager player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Crouch Move");

        player.playerController.EnterCrouch();
    }

    public void ExitState()
    {
        player.playerController.ExitCrouch();
    }

    public void FixedUpdateState()
    {
        if (player.inputController.inputY < 0 && player.inputController.inputX != 0)
            player.playerController.Movement();
    }

    public void UpdateState()
    {
        if (player.inputController.inputY >= 0)
            player.SwitchState(player.idleState);

        if (player.inputController.inputY < 0 && player.inputController.inputX == 0)
            player.SwitchState(player.crouchState);

        PlayerOneWayPlatform.instance.GetThroughOneWayPlatform();
    }


}
