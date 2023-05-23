using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossfadeEffect : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        PlayCrossfadeIn();
    }

    void PlayCrossfadeIn()
    {
        animator.Play("Crossfade_In");
    }
    public void PlayCrossfadeOut()
    {
        animator.Play("Crossfade_Out");
    }
}
