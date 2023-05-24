using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerController playerController;

    [Header("Player Animation")]
    Animator animator;
    public AnimatorStateInfo currentState;
    public string[] state = {
        "Idle",
        "Run",
        "Idle To Run",
        "Run To Idle",
        "Jump",
        "On Air",
        "Fall",
        "Hurt",
        "Wall Jump",
        "Wall Side",
        "Death"
    };
    public string IDLE = "Idle";
    public string RUN = "Run";
    public string IDLE_RUN = "Idle To Run";
    public string RUN_IDLE = "Run To Idle";
    public string JUMP = "Jump";
    public string ONAIR = "On Air";
    public string FALL = "Fall";
    public string HURT = "Hurt";
    public string WALL_JUMP = "Wall Jump";
    public string WALL_SIDE = "Wall Side";
    public string[] ATTACK = { "Attack 1", "Attack 2", "Attack 3" };
    public string DEATH = "Death";
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        currentState = animator.GetCurrentAnimatorStateInfo(0);

        WallSlideAnim();
        FallingAnim();
        Acceleration();
        BeDeathAnim();
        BeDamagedAnim();
    }
    #region Animation
    public void PlayAnimation(string clip)
    {
        if (CheckCurrentAnimation(DEATH)) return;

        if (CheckCurrentAnimation(HURT) && currentState.normalizedTime < 1) return;

        if (currentState.IsName(clip)) return;

        animator.CrossFade(clip, 0, 0);
    }
    public bool CheckCurrentAnimation(string clip)
    {
        if (currentState.IsName(clip)) return true;
        else return false;
    }
    public void MoveAnim(string clip)
    {
        if (CheckCurrentAnimation(ATTACK[0]) || CheckCurrentAnimation(ATTACK[1]) || CheckCurrentAnimation(ATTACK[2]))
        {
            if (currentState.normalizedTime < 1) return;
        }

        if (CheckCurrentAnimation(JUMP) || CheckCurrentAnimation(WALL_JUMP)) return;

        if (playerController.playerProperties.isGrounded)
            PlayAnimation(clip);
    }
    #endregion

    #region Hurt and Die
    public void BeDamagedAnim()
    {
        if (!playerController.playerProperties.isHurt) return;

        PlayAnimation(HURT);

        MoveAnim(IDLE);

        playerController.playerProperties.isHurt = false;
    }

    public void BeDeathAnim()
    {
        if (playerController.playerProperties.isDead)
            PlayAnimation(DEATH);
    }
    #endregion

    public void WallSlideAnim()
    {
        if (playerController.playerProperties.isGrounded) return;

        if (playerController.playerProperties.isLeftWall || playerController.playerProperties.isRightWall)
        {
            PlayAnimation(WALL_SIDE);
        }
    }

    void FallingAnim()
    {
        if (playerController.playerProperties.isGrounded || playerController.playerProperties.isLeftWall || playerController.playerProperties.isRightWall) return;

        if (playerController.playerRigid.velocity.y <= -1f)
        {
            PlayAnimation(FALL);
        }
        else if (Mathf.Abs(playerController.playerRigid.velocity.y) < 1f)
            PlayAnimation(ONAIR);
    }

    void Acceleration()
    {
        if (Mathf.Abs(playerController.playerProperties.inputX) == 0)
            MoveAnim(IDLE);

        else if (Mathf.Abs(playerController.playerProperties.inputX) == 1)
            MoveAnim(RUN);

        else if (Mathf.Abs(playerController.playerProperties.inputX) < 0.4)
        {
            if (playerController.playerRigid.velocity.x == 0)
                MoveAnim(IDLE_RUN);

            else if (playerController.playerRigid.velocity.x != 0)
                MoveAnim(RUN_IDLE);
        }
    }
}
