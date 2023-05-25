using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : IState
{
    public void EnterState(PlayerStateManager player)
    {
        player.playerAnimation.PlayAnimatorClip("Attack Jump 1");

        player.playerAttack.AttackCast(3);
    }

    public void ExitState(PlayerStateManager player)
    {
    }

    public void FixedUpdateState(PlayerStateManager player)
    {
    }

    public void UpdateState(PlayerStateManager player)
    {
        if (player.playerAnimation.currentState.normalizedTime <= 1) return;

        player.SwitchState(player.fallState);
    }

}
