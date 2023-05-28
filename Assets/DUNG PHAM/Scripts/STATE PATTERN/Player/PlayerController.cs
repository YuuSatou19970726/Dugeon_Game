using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IStopAttack
{
    #region MISCELLANEOUS
    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    PlayerWallSlideAndJump playerWall;
    PlayerCollisionDetector playerCollision;
    Rigidbody2D playerRigid;
    Collider2D playerColi;
    bool unGravity = false;
    void GetObjectComponents()
    {
        playerCollision = GetComponent<PlayerCollisionDetector>();
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerWall = GetComponent<PlayerWallSlideAndJump>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerColi = GetComponent<Collider2D>();
    }

    public bool StopAttack()
    {
        return playerDatabase.isDied;
    }
    #endregion

    #region MONOBEHAVIOUR
    void Awake()
    {
        GetObjectComponents();
    }
    void Start()
    {
        UnGravity(0.5f);
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
        if (unGravity)
        {
            playerRigid.gravityScale = 0;
            return;
        }

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

    public void UnGravity(float time)
    {
        StartCoroutine(UnGravityCoroutine(time));
    }
    IEnumerator UnGravityCoroutine(float time)
    {
        unGravity = true;
        yield return new WaitForSeconds(time);
        unGravity = false;
    }


    void MaxFallVelocity()
    {
        playerRigid.velocity = new Vector2(playerRigid.velocity.x, Mathf.Max(playerRigid.velocity.y, -playerDatabase.maxFallVelocity));
    }
    #endregion



}
