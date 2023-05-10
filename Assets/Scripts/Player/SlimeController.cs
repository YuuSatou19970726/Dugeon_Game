using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    CameraController cameraController;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    Animator animator;

    float xDirection;

    bool isJumping = false;
    float moveSpeed = 5f;
    float jumpForce = 5f;

    [SerializeField]
    LayerMask jumpableGround;

    //Animation States
    string currentState = "Slime_Idle_Animation";
    const string PLAYER_IDLE = "Slime_Idle_Animation";
    const string PLAYER_JUMP_START_UP = "Slime_Jump_Start_Up";
    const string PLAYER_HOLD_JUMP = "Slime_Hold_Jump";
    const string PLAYER_JUMP_UP = "Slime_Jump_Up";
    const string PLAYER_JUMP_TO_FALL = "Slime_Jump_To_Fall";
    const string PLAYER_JUMP_DOWN = "Slime_Jump_Down";
    const string PLAYER_JUMP_LAND = "Slime_Jump_Land";
    const string PLAYER_HURT = "Slime_Hurt";
    const string PLAYER_DEATH = "Slime_Death";

    float animatorDeplay = 0.3f;

    private void Awake()
    {
        cameraController = FindAnyObjectByType<CameraController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PLAYER_IDLE)
        {
            moveSpeed = 5f;
        }
        else if (currentState == PLAYER_HOLD_JUMP)
        {
            moveSpeed = 3f;
        }
        else
        {
            moveSpeed = 0f;
        }

        SlimeMove();
        SlimeJump();
        cameraController.SetMoveCamera(transform);
    }

    private void FixedUpdate()
    {
        if (xDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (xDirection < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (isJumping)
        {
            isJumping = false;
            animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AnimationJump", animatorDeplay);
        }
    }

    void SlimeMove()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * xDirection * Time.deltaTime;

        if(moveSpeed != 0 && IsGrounded())
            transform.position = transform.position + new Vector3(moveStep, 0, 0);

        if (moveSpeed == 0f && !IsGrounded())
        {
            if (spriteRenderer.flipX)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }
            moveStep = 5f * xDirection * Time.deltaTime;
            transform.position = transform.position + new Vector3(moveStep, 0, 0);
        }
    }

    void SlimeJump()
    {

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            isJumping = false;
            if (currentState != PLAYER_HOLD_JUMP)
            {
                ChangeAnimationState(PLAYER_JUMP_START_UP);
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("AnimationHoldJump", animatorDeplay);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && IsGrounded())
        {
            isJumping = true;
        }

        if (currentState == PLAYER_HOLD_JUMP)
        {
            if (jumpForce < 7f)
                jumpForce += (0.5f * Time.deltaTime);
        }
    }

    void AnimationHoldJump()
    {
        if (currentState == PLAYER_JUMP_START_UP)
        {
            ChangeAnimationState(PLAYER_HOLD_JUMP);
        }
    }

    void AnimationJump()
    {
        switch (currentState)
        {
            case PLAYER_JUMP_START_UP:
                if (IsGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMP_UP);
                    rigid.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
                } else
                {
                    ChangeAnimationState(PLAYER_IDLE);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                }
                break;
            case PLAYER_HOLD_JUMP:
                if (IsGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMP_UP);
                    rigid.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
                } else
                {
                    ChangeAnimationState(PLAYER_IDLE);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                }
                break;
            case PLAYER_JUMP_UP:
                ChangeAnimationState(PLAYER_JUMP_TO_FALL);
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                break;
            case PLAYER_JUMP_TO_FALL:
                ChangeAnimationState(PLAYER_JUMP_DOWN);
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                break;
            case PLAYER_JUMP_DOWN:
                if (IsGrounded())
                {
                    ChangeAnimationState(PLAYER_JUMP_LAND);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                } else
                {
                    animatorDeplay = 0f;
                }
                break;
            case PLAYER_JUMP_LAND:
                ChangeAnimationState(PLAYER_IDLE);
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                jumpForce = 5f;
                break;
        }

        if (currentState != PLAYER_IDLE)
            Invoke("AnimationJump", animatorDeplay);
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
