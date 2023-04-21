using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;

    Animator animator;

    float xDirection;

    bool isJumping = false;
    float moveSpeed = 5f;
    float jumpForce = 5f;

    [SerializeField]
    LayerMask jumpableGround;

    //Animation States
    const string PLAYER_IDLE = "Slime_Idle";
    const string PLAYER_JUMP = "Slime_Jump_Start_Up";
    const string PLAYER_HURT = "Slime_Hurt";
    const string PLAYER_DEATH = "Slime_Death";

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
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
        if (isJumping)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            isJumping = false;
            ChangeAnimationState(PLAYER_JUMP);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void ChangeAnimationState(string animation)
    {
        animator.Play(animation);
    }
}
