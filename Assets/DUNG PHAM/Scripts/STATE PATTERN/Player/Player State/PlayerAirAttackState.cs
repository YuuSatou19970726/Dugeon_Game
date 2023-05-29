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
        player.soundEffect.PlayAudio(6);
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
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerAnimation.currentState.normalizedTime <= 1) return;

        player.SwitchState(player.fallState);
    }

}
