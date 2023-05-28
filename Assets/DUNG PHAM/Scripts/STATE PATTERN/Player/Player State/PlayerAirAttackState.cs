using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : IState
{
    PlayerStateManager player;
    PlayerAttackManager playerAttack;
    public PlayerAirAttackState(PlayerStateManager player, PlayerAttackManager playerAttack)
    {
        this.player = player;
        this.playerAttack = playerAttack;
    }

    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip("Attack Jump 1");

        playerAttack.AttackCast(3);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        if (player.playerAnimation.currentState.normalizedTime <= 1) return;

        player.SwitchState(player.fallState);
    }

}
