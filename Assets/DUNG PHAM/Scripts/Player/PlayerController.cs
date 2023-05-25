using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IStopAttack
{
    #region MISCELLANEOUS
    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    Rigidbody2D playerRigid;
    Collider2D playerColi;
    PlayerWallSlideAndJump playerWall;
    PlayerWallLedgeGrabAndClimb playerLedge;
    PlayerCollisionDetector playerCollision;
    public bool StopAttack()
    {
        return playerDatabase.isDied;
    }
    #endregion

    #region MONOBEHAVIOUR
    void Awake()
    {
        playerCollision = GetComponent<PlayerCollisionDetector>();
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerLedge = GetComponent<PlayerWallLedgeGrabAndClimb>();
        playerWall = GetComponent<PlayerWallSlideAndJump>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerColi = GetComponent<Collider2D>();
    }

    void Update()
    {
        FallGravityChange();
        MaxFallVelocity();
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
        if (playerWall.wallTimer <= 0.4f)
        {
            playerRigid.gravityScale = playerDatabase.gravity / 2;
            return;
        }

        if (playerCollision.isLeftEdge || playerCollision.isRightEdge)
        {
            playerRigid.gravityScale = 0;
            return;
        }

        if (playerRigid.velocity.y < 0)
        {
            if (playerCollision.isLeftWall || playerCollision.isRightWall)
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
