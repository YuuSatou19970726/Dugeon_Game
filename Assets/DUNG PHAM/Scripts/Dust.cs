using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    public string IDLE = "Dust Idle";
    public string JUMP = "Dust Jump";
    public string RUN = "Dust Run";
    public string LAND = "Dust Land";
    public string DASH = "Dust Dash";
    Animator animator;

    PlayerController playerController;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }
    void Update()
    {

    }
    void PlayAnimation(string strg)
    {
        animator.CrossFade(strg, 0);
    }
    public void PlayIdle()
    {
        PlayAnimation(IDLE);
    }
    public void PlayJump()
    {
        PlayAnimation(JUMP);
        StartCoroutine(AnimationDelay());
    }
    public void PlayRun()
    {

        PlayAnimation(RUN);
        StartCoroutine(AnimationDelay());
    }
    public void PlayLand()
    {
        PlayAnimation(LAND);
        StartCoroutine(AnimationDelay());
    }
    public void PlayDash()
    {
        PlayAnimation(DASH);

        StartCoroutine(AnimationDelay());
    }
    IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(0.5f);
        PlayIdle();
    }
}
