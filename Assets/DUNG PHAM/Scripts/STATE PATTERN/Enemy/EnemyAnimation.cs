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
        animator.Play(animationClips[index].name);
    }
    public bool CheckState(int index)
    {
        if (currentState.IsName(animationClips[index].name))
            return true;
        return false;
    }

}
