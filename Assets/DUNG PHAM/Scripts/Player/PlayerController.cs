using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IStopAttack
{
    #region MISCELLANEOUS
    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    PlayerJump playerJump;
    PlayerDash playerDash;
    PlayerWallSlideAndJump playerWallSlideAndJump;
    PlayerWallLedgeGrabAndClimb playerWallLedgeGrabAndClimb;
    Rigidbody2D playerRigid;
    Collider2D playerColi;
    Vector2 groundCheckPoint;


    public bool StopAttack()
    {
        return playerDatabase.isDied;
    }
    #endregion

    #region MONOBEHAVIOUR
    void Awake()
    {
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerJump = GetComponent<PlayerJump>();
        playerDash = GetComponent<PlayerDash>();
        playerWallSlideAndJump = GetComponent<PlayerWallSlideAndJump>();
        playerWallLedgeGrabAndClimb = GetComponent<PlayerWallLedgeGrabAndClimb>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerColi = GetComponent<Collider2D>();
    }

    void Update()
    {
        GroundCheck();

        FallGravityChange();
        MaxFallVelocity();
    }

    #endregion

    #region COLLISION DETECT
    void GroundCheck()
    {
        groundCheckPoint = playerColi.bounds.center - new Vector3(0f, playerColi.bounds.size.y / 2, 0f);

        if (Physics2D.OverlapCircle(groundCheckPoint, 0.1f, playerDatabase.groundLayer))
            playerDatabase.isGrounded = true;
        else
            playerDatabase.isGrounded = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint, 0.1f);
    }
    #endregion

    #region HORIZONTAL MOVEMENT

    public void Movement()
    {
        playerRigid.velocity = new Vector2(playerDatabase.moveSpeed * inputController.inputX, playerRigid.velocity.y);

        if (inputController.inputXRaw != 0) transform.localScale = new Vector2(inputController.inputXRaw, 1);
    }

    public void MoveOnAir()
    {
        playerRigid.velocity = new Vector2(playerDatabase.moveSpeed * inputController.inputX, playerRigid.velocity.y);

        if (inputController.inputXRaw != 0) transform.localScale = new Vector2(inputController.inputXRaw, 1);
    }
    #endregion

    #region GROUND JUMP

    public void Jump() =>
        playerJump.Jump();

    public void StopJump(float time) =>
        playerJump.StopJump(time);

    #endregion

    #region DASH

    public void Dash() =>
        playerDash.Dash();

    #endregion

    #region WALL
    public void WallSlide() =>
        playerWallSlideAndJump.WallSlide();

    public bool isWallJumping;
    public void WallJump() =>
        playerWallSlideAndJump.WallJump();

    public void WallEdgeGrab() =>
        playerWallLedgeGrabAndClimb.WallEdgeGrab();

    public void WallClimb() =>
        playerWallLedgeGrabAndClimb.WallClimb();

    #endregion

    #region CROUCH
    public void EnterCrouch()
    {
        playerDatabase.head.position = transform.position;
    }

    public void ExitCrouch()
    {
        playerDatabase.head.position = transform.position + Vector3.up / 2;
    }
    #endregion

    #region GRAVITY
    void FallGravityChange()
    {
        if (playerWallSlideAndJump.wallTimer <= 0.4f)
        {
            playerRigid.gravityScale = playerDatabase.gravity / 2;
            return;
        }

        if (playerDatabase.isLeftEdge || playerDatabase.isRightEdge)
        {
            playerRigid.gravityScale = 0;
            return;
        }

        if (playerRigid.velocity.y < 0)
        {
            if (playerDatabase.isLeftWall || playerDatabase.isRightWall)
            {
                playerRigid.gravityScale = playerDatabase.gravity / 2;
                return;
            }

            playerRigid.gravityScale = playerDatabase.gravity * 2;

            return;
        }


        playerRigid.gravityScale = playerDatabase.gravity;
    }


    void MaxFallVelocity()
    {
        playerRigid.velocity = new Vector2(playerRigid.velocity.x, Mathf.Max(playerRigid.velocity.y, -playerDatabase.maxFallVelocity));
    }
    #endregion
}
