using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationNew : MonoBehaviour
{
    PlayerController playerController;
    Animator animator;
    public enum PlayerAnimationState { Idle, Run, IdleToRun, RunToIdle, Jump, OnAir, Fall, Hurt, WallJump, WallSide, Attack1, Attack2, Attack3, Death }
    public PlayerAnimationState state;
    public AnimatorStateInfo currentState;
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
        WallSlideAnim();
    }

    #region Animation
    public void PlayAnimation(PlayerAnimationState state)
    {
        if (CheckCurrentAnimation(PlayerAnimationState.Death)) return;

        if (CheckCurrentAnimation(state)) return;

        Debug.Log(state.ToString());

        animator.CrossFade(state.ToString(), 0, 0);
    }

    public bool CheckCurrentAnimation(PlayerAnimationState state)
    {
        if (currentState.IsName(state.ToString())) return true;
        else return false;
    }

    public void MoveAnim(PlayerAnimationState state)
    {
        if (CheckCurrentAnimation(PlayerAnimationState.Attack1)
         || CheckCurrentAnimation(PlayerAnimationState.Attack2)
         || CheckCurrentAnimation(PlayerAnimationState.Attack3)
         || CheckCurrentAnimation(PlayerAnimationState.Hurt))
        {
            if (currentState.normalizedTime < 1) return;
        }

        if (CheckCurrentAnimation(PlayerAnimationState.Jump) || CheckCurrentAnimation(PlayerAnimationState.WallJump)) return;

        if (playerController.playerProperties.isGrounded)
            PlayAnimation(state);
    }
    #endregion

    #region Hurt and Die
    public void BeDamagedAnim()
    {
        PlayAnimation(PlayerAnimationState.Hurt);

        MoveAnim(PlayerAnimationState.Idle);
    }

    public void BeDeathAnim()
    {
        PlayAnimation(PlayerAnimationState.Death);
    }
    #endregion

    public void WallSlideAnim()
    {
        if (playerController.playerProperties.isGrounded) return;

        if (playerController.playerProperties.isLeftWall || playerController.playerProperties.isRightWall)
        {
            PlayAnimation(PlayerAnimationState.WallSide);
        }
    }

}
