using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
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
    const string PLAYER_JUMP_UP = "Slime_Jump_Up";
    const string PLAYER_JUMP_TO_FALL = "Slime_Jump_To_Fall";
    const string PLAYER_JUMP_DOWN = "Slime_Jump_Down";
    const string PLAYER_JUMP_LAND = "Slime_Jump_Land";
    const string PLAYER_HURT = "Slime_Hurt";
    const string PLAYER_DEATH = "Slime_Death";

    string currentState;

    float animatorDeplay = 0.3f;

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
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * xDirection * Time.deltaTime;
        transform.position = transform.position + new Vector3(moveStep, 0, 0);

        if(Input.GetButtonDown("Jump") && !isJumping && IsGrounded())
        {
            ChangeAnimationState(PLAYER_JUMP_START_UP);
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if(xDirection > 0)
        {
            spriteRenderer.flipX = false;
        } else if (xDirection < 0)
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

    void AnimationJump()
    {
        if (PLAYER_STATE == PLAYER_JUMP_UP)
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
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

    void AnimationState()
    {
        ChangeAnimationState(PLAYER_STATE);
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
