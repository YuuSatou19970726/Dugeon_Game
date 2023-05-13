using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    public AnimatorStateInfo currentState;
    public AnimationClip[] animationClips;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);

    }
    public void PlayAnimation(int index)
    {
        if (CheckState(index)) return;

        // if (CheckState(7)) return;                                      // Die Animation

        // if (CheckState(6) && currentState.normalizedTime < 1) return;   // Hurt Animation

        // if (CheckState(5) && currentState.normalizedTime < 1) return;   // Attack Animation

        animator.Play(animationClips[index].name);
    }
    public bool CheckState(int index)
    {
        if (currentState.IsName(animationClips[index].name))
            return true;
        return false;
    }

}
