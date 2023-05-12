using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    public AnimatorStateInfo currentState;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);
    }

    public void PlayAnimatorClip(string clip)
    {
        animator.CrossFade(clip, 0);
    }
}
