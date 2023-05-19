using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    BaseCurrent baseCurrent;
    CameraController cameraController;
    BoardTutorial boardTutorial;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    //audio
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioClip;

    //animator
    Animator animator;

    //value move
    float xDirection;

    bool isJumping = false;
    float moveSpeed = 5f;
    float jumpForce = 5f;

    [SerializeField]
    LayerMask jumpableGround;

    //Animation States
    string currentState = "Slime_Idle_Animation";

    float animatorDeplay = 0.3f;

    private void Awake()
    {
        cameraController = FindAnyObjectByType<CameraController>();
        boardTutorial = FindAnyObjectByType<BoardTutorial>();
    }

    // Start is called before the first frame update
    void Start()
    {
        baseCurrent = gameObject.AddComponent<BaseCurrent>();

        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == baseCurrent.GetPlayerIdle() || currentState == baseCurrent.GetPlayerMove())
        {
            moveSpeed = 5f;
        }
        else if (currentState == baseCurrent.GetPlayerJumpStartUp() || currentState == baseCurrent.GetPlayerHoldJump() ||
            currentState == baseCurrent.GetPlayerHoldJumpGreen() || currentState == baseCurrent.GetPlayerHoldJumpRed())
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
        boardTutorial.CheckTransform(transform);
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

        if (xDirection != 0 && IsGrounded() && currentState == baseCurrent.GetPlayerIdle())
        {
            ChangeAnimationState(baseCurrent.GetPlayerMove());
        } else if (xDirection == 0 && currentState == baseCurrent.GetPlayerMove() && IsGrounded())
        {
            ChangeAnimationState(baseCurrent.GetPlayerIdle());
        }

        if (isJumping)
        {
            isJumping = false;
            if (IsGrounded())
            {
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("AnimationJump", animatorDeplay);
            } else
            {
                ChangeAnimationState(baseCurrent.GetPlayerIdle());
            }
        }
    }

    void SlimeMove()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = moveSpeed * xDirection * Time.deltaTime;

        if (moveSpeed != 0 && IsGrounded() && xDirection != 0)
        {
            ChangeAnimationState(baseCurrent.GetPlayerMove());
            transform.position = transform.position + new Vector3(moveStep, 0, 0);
        }

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

        if (Input.GetKey(KeyCode.Space) && IsGrounded() && currentState == baseCurrent.GetPlayerIdle())
        {
            isJumping = false;
            if (currentState != baseCurrent.GetPlayerHoldJump())
            {
                ChangeAnimationState(baseCurrent.GetPlayerJumpStartUp());
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("AnimationHoldJump", animatorDeplay);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (currentState == baseCurrent.GetPlayerMove())
            {
                isJumping = false;
            } else
            {
                if (currentState == baseCurrent.GetPlayerJumpStartUp() || currentState == baseCurrent.GetPlayerHoldJump() ||
                currentState == baseCurrent.GetPlayerHoldJumpGreen() || currentState == baseCurrent.GetPlayerHoldJumpRed())
                    isJumping = true;
            }
        }

        if (currentState == baseCurrent.GetPlayerHoldJump() || currentState == baseCurrent.GetPlayerHoldJumpGreen() || currentState == baseCurrent.GetPlayerHoldJumpRed())
        {
            if (jumpForce < 7f)
                jumpForce += (0.5f * Time.deltaTime);

             if (jumpForce >= 6f && jumpForce < 7f && currentState == baseCurrent.GetPlayerHoldJump())
            {
                ChangeAnimationState(baseCurrent.GetPlayerHoldJumpGreen());
            } else if (jumpForce > 7f && currentState == baseCurrent.GetPlayerHoldJumpGreen())
            {
                ChangeAnimationState(baseCurrent.GetPlayerHoldJumpRed());
            }
        }
    }

    void AnimationHoldJump()
    {
        if (currentState == baseCurrent.GetPlayerJumpStartUp())
        {
            ChangeAnimationState(baseCurrent.GetPlayerHoldJump());
        }
    }

    void AnimationJump()
    {
        switch (currentState)
        {
            case "Slime_Jump_Start_Up":
                if (IsGrounded())
                {
                    ChangeAnimationState(baseCurrent.GetPlayerJumpUp());
                    rigid.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
                    audioSource.PlayOneShot(audioClip);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
                } else
                {
                    ChangeAnimationState(baseCurrent.GetPlayerIdle());
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                }
                break;
            case "Slime_Hold_Jump":
                if (IsGrounded())
                {
                    ChangeAnimationState(baseCurrent.GetPlayerJumpUp());
                    rigid.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
                    audioSource.PlayOneShot(audioClip);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
                } else
                {
                    ChangeAnimationState(baseCurrent.GetPlayerIdle());
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                }
                break;
            case "Slime_Hold_Jump_Green":
                if (IsGrounded())
                {
                    ChangeAnimationState(baseCurrent.GetPlayerJumpUp());
                    rigid.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
                    audioSource.PlayOneShot(audioClip);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
                }
                else
                {
                    ChangeAnimationState(baseCurrent.GetPlayerIdle());
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                }
                break;
            case "Slime_Hold_Jump_Red":
                if (IsGrounded())
                {
                    ChangeAnimationState(baseCurrent.GetPlayerJumpUp());
                    rigid.AddForce(Vector3.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
                    audioSource.PlayOneShot(audioClip);
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.5f;
                }
                else
                {
                    ChangeAnimationState(baseCurrent.GetPlayerIdle());
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                }
                break;
            case "Slime_Jump_Up":
                ChangeAnimationState(baseCurrent.GetPlayerJumpToFall());
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                break;
            case "Slime_Jump_To_Fall":
                if (IsGrounded())
                {
                    ChangeAnimationState(baseCurrent.GetPlayerIdle());
                } else
                {
                    ChangeAnimationState(baseCurrent.GetPlayerJumpDown());
                }
                animatorDeplay = 0f;
                break;
            case "Slime_Jump_Down":
                if (IsGrounded())
                {
                    ChangeAnimationState(baseCurrent.GetPlayerJumpLand());
                    animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                } else
                {
                    animatorDeplay = 0f;
                }
                break;
            case "Slime_Jump_Land":
                ChangeAnimationState(baseCurrent.GetPlayerIdle());
                animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                jumpForce = 5f;
                break;
        }

        if (currentState != baseCurrent.GetPlayerIdle())
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
