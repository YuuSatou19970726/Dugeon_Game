using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player_Animator : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    Player_Movement player_Movement;
    public AnimationClip[] animationClips;
    AnimatorStateInfo state;
    float lockStill;
    void Awake()
    {
    }
    void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);
    }
    public void AnimationPlay(int index)
    {
        // if (state.IsName(animationClips[index].name)) return;

        // animator.Play(animationClips[index].name);
        animator.CrossFade(animationClips[index].name, 0f);
    }
    public void IsMoving()
    {
        if (state.IsName(animationClips[5].name) || state.IsName(animationClips[6].name) || state.IsName(animationClips[7].name)) return;

        if (player_Movement.GroundCheck())
        {
            if (player_Movement.input != 0)
            {
                AnimationPlay(1);
            }
            else AnimationPlay(0);
        }
    }
    public void IsJumping()
    {
        if (state.IsName(animationClips[5].name) || state.IsName(animationClips[6].name) || state.IsName(animationClips[7].name)) return;

        float v = player_Movement.GetComponent<Rigidbody2D>().velocity.y;
        if (v > 1f)
        {
            AnimationPlay(2);
        }
        else if (v < -1f)
        {
            AnimationPlay(4);
        }
        else if (!player_Movement.GroundCheck())
        {
            AnimationPlay(3);
        }
    }
    public void IsHurt()
    {
        AnimationPlay(10);
    }
    public void IsDeath()
    {
        AnimationPlay(11);
    }
}
