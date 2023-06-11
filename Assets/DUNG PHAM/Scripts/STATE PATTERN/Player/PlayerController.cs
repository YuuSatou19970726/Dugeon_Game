using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IStopAttack
{
    #region MISCELLANEOUS
    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    PlayerCollisionDetector playerCollision;
    Rigidbody2D playerRigid;
    Collider2D playerColi;
    bool unGravity = false;

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void GetObjectComponents()
    {
        playerCollision = GetComponent<PlayerCollisionDetector>();
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerColi = GetComponent<Collider2D>();
    }

    public bool StopAttack()
    {
        return playerDatabase.isDied;
    }
    #endregion

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

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
        CoyoteJumpCheck();

        CooldownTimer();
        JumpCut();
        JumpReset();
    }
    #endregion

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

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

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

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

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    #region GRAVITY
    void FallGravityChange()
    {
        if (unGravity || playerCollision.isLeftEdge || playerCollision.isRightEdge)
        {
            playerRigid.gravityScale = 0;
            return;
        }

        if (wallTimer <= 0.4f)
        {
            playerRigid.gravityScale = playerDatabase.gravity / 2;
            return;
        }

        if (playerRigid.velocity.y < 0)
        {
            if (playerCollision.isLeftWall || playerCollision.isRightWall)
            {
                playerRigid.gravityScale = playerDatabase.gravity;
                return;
            }

            playerRigid.gravityScale += playerDatabase.gravity * 2 * Time.deltaTime;
            return;
        }


        playerRigid.gravityScale = playerDatabase.gravity;
    }

    /**********************************************************************************************************************************/

    public void UnGravity(float time)
    {
        StartCoroutine(UnGravityCoroutine(time));
    }
    IEnumerator UnGravityCoroutine(float time)
    {
        unGravity = true;
        yield return new WaitForSecondsRealtime(time);
        unGravity = false;
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void MaxFallVelocity()
    {
        playerRigid.velocity = new Vector2(playerRigid.velocity.x, Mathf.Max(playerRigid.velocity.y, -playerDatabase.maxFallVelocity));
    }
    #endregion

    /**********************************************************************************************************************************/

    public float wallTimer;
    public float jumpTimer;
    public float dashTimer;
    void CooldownTimer()
    {
        jumpTimer += Time.deltaTime;
        wallTimer += Time.deltaTime;
        dashTimer += Time.deltaTime;
    }
    /**********************************************************************************************************************************/

    public int jumpCount;
    void JumpReset()
    {
        if (Input.GetKey(KeyCode.Space)) return;

        if (playerCollision.isGrounded) jumpCount = playerDatabase.jumpNumber;
    }

    public void StopJump(float time)
    {
        StartCoroutine(StopJumpCoroutine(time));
    }

    IEnumerator StopJumpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        playerRigid.velocity = new Vector2(playerRigid.velocity.x, 0);
    }

    void JumpCut()
    {
        if (jumpTimer > 0.2f) return;

        if (Input.GetKeyUp(KeyCode.Space))
            StartCoroutine(StopJumpCoroutine(0));
    }

    /*************************************************** COYOTE JUMP ******************************************************************/
    /**********************************************************************************************************************************/

    float lastGroundTimer;

    void CoyoteJumpCheck()
    {
        if (playerCollision.isGrounded)
            lastGroundTimer = 0f;
        else lastGroundTimer += Time.deltaTime;

        if (lastGroundTimer <= playerDatabase.maxCoyoteTime)
            playerCollision.isGrounded = true;
    }
}
