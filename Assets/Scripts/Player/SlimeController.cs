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
    string PLAYER_STATE = "Slime_Idle_Animation";
    const string PLAYER_IDLE = "Slime_Idle_Animation";
    const string PLAYER_JUMP_START_UP = "Slime_Jump_Start_Up";
    const string PLAYER_HOLD_JUMP = "Slime_Hold_Jump";
    const string PLAYER_JUMP_UP = "Slime_Jump_Up";
    const string PLAYER_JUMP_TO_FALL = "Slime_Jump_To_Fall";
    const string PLAYER_JUMP_DOWN = "Slime_Jump_Down";
    const string PLAYER_JUMP_LAND = "Slime_Jump_Land";
    const string PLAYER_HURT = "Slime_Hurt";
    const string PLAYER_DEATH = "Slime_Death";

    string currentState;

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
        SlimeMove();
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
            PLAYER_STATE = PLAYER_JUMP_UP;
            animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AnimationJump", animatorDeplay);
        }
    }

    void SlimeMove()
    {
        //move
        if (PLAYER_STATE == PLAYER_IDLE)
        {
            moveSpeed = 5f;
        }
        else if (PLAYER_STATE == PLAYER_JUMP_START_UP || PLAYER_STATE == PLAYER_HOLD_JUMP)
        {
            moveSpeed = 3f;
        }
        else
        {
            moveSpeed = 0f;
        }

        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * xDirection * Time.deltaTime;

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
            moveStep = 5 * xDirection * Time.deltaTime;
            transform.position = transform.position + new Vector3(moveStep, 0, 0);
        }
        else
        {
            transform.position = transform.position + new Vector3(moveStep, 0, 0);
        }

        //jump
        if (Input.GetKey(KeyCode.Space) && !isJumping && IsGrounded() && PLAYER_STATE == PLAYER_IDLE)
        {
            ChangeAnimationState(PLAYER_JUMP_START_UP);
            isJumping = false;

            PLAYER_STATE = PLAYER_JUMP_START_UP;
            animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AnimationHoldJump", animatorDeplay);
        } else if (Input.GetKeyDown(KeyCode.Space) && !isJumping && IsGrounded())
        {
            ChangeAnimationState(PLAYER_JUMP_START_UP);
            isJumping = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !isJumping && IsGrounded())
        {
            isJumping = true;
        }

        if (PLAYER_STATE == PLAYER_HOLD_JUMP)
        {
            if (jumpForce <= 7f)
            jumpForce += 0.5f;
        }
    }

    void AnimationHoldJump()
    {
        if (PLAYER_STATE == PLAYER_JUMP_START_UP)
        {
            PLAYER_STATE = PLAYER_HOLD_JUMP;
            ChangeAnimationState(PLAYER_STATE);
        }
    }

    void AnimationJump()
    {
        if (PLAYER_STATE == PLAYER_JUMP_UP && IsGrounded())
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            jumpForce = 5f;
        }
        ChangeAnimationState(PLAYER_STATE);

        if (PLAYER_STATE == PLAYER_IDLE) return;

        switch (PLAYER_STATE)
        {
            case PLAYER_JUMP_UP:
                PLAYER_STATE = PLAYER_JUMP_TO_FALL;
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length - 0.3f;
                break;
            case PLAYER_JUMP_TO_FALL:
                PLAYER_STATE = PLAYER_JUMP_DOWN;
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                break;
            case PLAYER_JUMP_DOWN:
                PLAYER_STATE = PLAYER_JUMP_LAND;
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length - 0.3f;
                break;
            case PLAYER_JUMP_LAND:
                PLAYER_STATE = PLAYER_IDLE;
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
                break;
        }
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
