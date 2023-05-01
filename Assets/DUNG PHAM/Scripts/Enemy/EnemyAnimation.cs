using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    EnemyController enemyController;
    Animator animator;
    public AnimatorStateInfo currentState;
    public AnimationClip[] animationClip;
    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);

        BeDeadAnimation();
        AttackAnimation();
        MoveAnimation();
        BeAttackedAnimation();
    }
    void PlayAnimation(int index)
    {
        if (!animationClip[index]) return;

        if (CheckState(index)) return;

        if (CheckState(7)) return;                                      // Die Animation

        if (CheckState(6) && currentState.normalizedTime < 1) return;   // Hurt Animation

        if (CheckState(5) && currentState.normalizedTime < 1) return;   // Attack Animation

        animator.Play(animationClip[index].name);
    }
    bool CheckState(int index)
    {
        if (currentState.IsName(animationClip[index].name))
            return true;
        return false;
    }
    void MoveAnimation()
    {
        if (!enemyController.enemyProperties.isGrounded)
        {
            if (enemyController.enemyProperties.objectRigid.velocity.y > 0)
            {
                PlayAnimation(3); // Jump Animation
                return;
            }
            else if (enemyController.enemyProperties.objectRigid.velocity.y < 0)
            {
                PlayAnimation(4); // Fall Animation
                return;
            }
        }

        if (Mathf.Abs(enemyController.enemyProperties.objectRigid.velocity.x) <= 0.5f) PlayAnimation(0);   // Idle Animation
        else if (Mathf.Abs(enemyController.enemyProperties.objectRigid.velocity.x) < 3f) PlayAnimation(1); // Walk Animation
        else PlayAnimation(2);                                                                             // Run Animation
    }
    public void AttackAnimation()
    {
        if (enemyController.enemyProperties.isAttacking)
            PlayAnimation(5);

        if (CheckState(5) && currentState.normalizedTime >= 1)
            enemyController.enemyProperties.isAttacking = false;
    }
    public void BeAttackedAnimation()
    {
        if (!enemyController.enemyProperties.isHurting) return;

        PlayAnimation(6);

        if (CheckState(6) && currentState.normalizedTime >= 1)
            enemyController.enemyProperties.isHurting = false;

    }
    public void BeDeadAnimation()
    {
        if (!enemyController.enemyProperties.isDead) return;

        PlayAnimation(7);
    }
}
