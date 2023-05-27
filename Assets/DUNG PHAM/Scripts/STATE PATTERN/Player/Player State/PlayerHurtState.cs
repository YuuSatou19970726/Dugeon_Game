using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : IState
{
    PlayerStateManager player;
    PlayerAttackManager playerAttack;
    public PlayerHurtState(PlayerStateManager player, PlayerAttackManager playerAttack)
    {
        this.player = player;
        this.playerAttack = playerAttack;
    }


    string HURT = "Hurt";
    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(HURT);
    }

    public void ExitState()
    {
    }

    public void FixedUpdateState()
    {
        player.playerController.Movement();
    }

    public void UpdateState()
    {
        PriorityState();

        if (player.playerAnimation.currentState.IsName(HURT) && player.playerAnimation.currentState.normalizedTime > 1)
        {
            player.SwitchState(player.idleState);
            player.playerDatabase.isHurt = false;
        }
    }

    void PriorityState()
    {
        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);
    }
}
