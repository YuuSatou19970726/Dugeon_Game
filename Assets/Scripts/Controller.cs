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
    const string PLAYER_IDLE = "Slime_Idle_Animation";
    const string PLAYER_JUMP = "Slime_Jump_Start_Up";
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
        } else
        {
            if (IsGrounded())
            {
                AnimationIdle();
            }
        }

        if (isJumping)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            ChangeAnimationState(PLAYER_JUMP);
            isJumping = false;

            animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
            Invoke("AnimationIdle", animatorDeplay);
        }
    }

    void AnimationIdle()
    {
        ChangeAnimationState(PLAYER_IDLE);
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
