using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerProperties playerProperties;
    [HideInInspector] public Collider2D playerColi;
    [HideInInspector] public Rigidbody2D playerRigid;
    [HideInInspector] public PlayerAnimation playerAnimation;
    Vector3 lowPoint;
    float runSpeed;

    #region Monobehaviour
    void Awake()
    {
        playerColi = GetComponent<Collider2D>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    void Start()
    {
        playerProperties.isJumping = playerProperties.isWallJumping = playerProperties.isDead = false;
    }
    void Update()
    {
        CheckGround();
        CheckJumpCondition();
    }
    void FixedUpdate()
    {
        Movement();
        GroundJump();
    }
    #endregion

    #region Movement
    void Movement()
    {
        if (playerProperties.inputXRaw != 0)
            transform.localScale = new Vector3(playerProperties.inputXRaw, 1, 1);

        if (playerProperties.moveOnAir)
            playerRigid.velocity = new Vector2(playerProperties.inputX * playerProperties.maxSpeed, playerRigid.velocity.y);
    }

    void CheckJumpCondition()
    {
        if (!playerProperties.isJumpPress) return;

        if (playerProperties.isGrounded)
        {
            playerProperties.isJumping = true;
            playerProperties.moveOnAir = true;
        }
        else
        {
            if (playerProperties.isLeftWall || playerProperties.isRightWall)
                playerProperties.isWallJumping = true;

            playerProperties.moveOnAir = false;
        }

    }
    void GroundJump()
    {
        if (!playerProperties.isJumping) return;

        playerRigid.AddForce(Vector2.up * playerProperties.jumpForce, ForceMode2D.Impulse);

        playerProperties.isJumping = false;

        if (playerRigid.velocity.y > 1f)
        {
            playerAnimation.PlayAnimation(playerAnimation.JUMP);
        }
    }


    #endregion

    #region Check Environment
    void CheckGround()
    {
        lowPoint = playerColi.bounds.center - new Vector3(0f, playerColi.bounds.size.y / 2, 0f);

        if (Physics2D.OverlapCircle(lowPoint, 0.1f, playerProperties.groundLayer))
        {
            playerProperties.isGrounded = true;
            playerProperties.moveOnAir = true;
        }
        else
        {
            playerProperties.isGrounded = false;
            // playerProperties.moveOnAir = false;
        }
    }

    #endregion


    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(lowPoint, 0.1f);
    }
    #endregion
}
