using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public PlayerDatabase playerDatabase;
    InputControllerNew inputController;
    Rigidbody2D rigid;
    Vector2 groundCheckPoint;
    Vector2 leftWallCheckPoint, rightWallCheckPoint;
    public Transform head;

    public bool isGrounded;
    public bool isLeftWall, isRightWall;
    float lastGroundTime;
    [SerializeField] int jumpDirection;
    public bool onWall = false;
    float dashTimer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputControllerNew>();
    }

    void Update()
    {
        GroundCheck();
        WallCheck();
        FallGravityChange();
        JumpConditionCheck();

        dashTimer += Time.deltaTime;
    }
    public void Movement()
    {
        rigid.velocity = new Vector2(playerDatabase.moveSpeed * inputController.inputX, rigid.velocity.y);

        if (inputController.inputXRaw != 0) transform.localScale = new Vector2(inputController.inputXRaw, 1);
    }

    public void MoveOnAir()
    {
        rigid.velocity = new Vector2(playerDatabase.moveSpeed * inputController.inputX, rigid.velocity.y);

        if (inputController.inputXRaw != 0) transform.localScale = new Vector2(inputController.inputXRaw, 1);
    }


    public void Jump()
    {
        rigid.velocity = Vector2.up * playerDatabase.jumpForce;
    }

    void JumpConditionCheck()
    {
        if (isGrounded)
            lastGroundTime = 0f;
        else lastGroundTime += Time.deltaTime;

        if (lastGroundTime <= playerDatabase.maxCoyoteTime)
            isGrounded = true;
    }

    void FallGravityChange()
    {
        if (onWall)
        {
            rigid.gravityScale = 0.5f;
            return;
        }

        if (isGrounded)
        {
            rigid.gravityScale = 1f;
            return;
        }

        if (inputController.isJumpHold)
        {
            if (rigid.velocity.y >= 0)
                rigid.gravityScale = 1f;

            else rigid.gravityScale = 4f;
        }
        else
        {
            rigid.gravityScale = 7f;
        }
    }

    public void EnterCrouch()
    {
        head.transform.position = transform.position;
    }

    public void ExitCrouch()
    {
        head.transform.position = transform.position + Vector3.up / 2;
    }

    public void WallJump()
    {
        if (isLeftWall)
        {
            jumpDirection = 1;
        }
        if (isRightWall)
        {
            jumpDirection = -1;
        }

        Vector3 targetPosition = new Vector2(transform.position.x + playerDatabase.jumpDistance * jumpDirection, transform.position.y + playerDatabase.jumpHeight);

        StartCoroutine(MoveToTargetPosition(targetPosition));
    }

    public void Dash()
    {
        if (dashTimer < playerDatabase.dashingCooldown) return;

        if (inputController.inputXRaw != 0)
            rigid.AddForce(new Vector2(inputController.inputXRaw, inputController.inputYRaw) * playerDatabase.dashingPower, ForceMode2D.Impulse);
        else
            rigid.AddForce(new Vector2(transform.localScale.x * playerDatabase.dashingPower, 0f), ForceMode2D.Impulse);

        AfterImagePool.instance.DisplaySprite();

        dashTimer = 0;
    }

    private IEnumerator MoveToTargetPosition(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startingPos = transform.position;

        while (elapsedTime < playerDatabase.timeJump)
        {
            elapsedTime += Time.deltaTime * playerDatabase.moveSpeed;
            transform.position = Vector2.Lerp(startingPos, targetPosition, elapsedTime);
            yield return null;
        }
    }

    void GroundCheck()
    {
        groundCheckPoint = GetComponent<Collider2D>().bounds.center - new Vector3(0f, GetComponent<Collider2D>().bounds.size.y / 2, 0f);

        if (Physics2D.OverlapCircle(groundCheckPoint, 0.1f, playerDatabase.groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
    }
    void WallCheck()
    {
        leftWallCheckPoint = GetComponent<Collider2D>().bounds.center - new Vector3(GetComponent<Collider2D>().bounds.size.x / 2, -0.5f, 0f);
        rightWallCheckPoint = GetComponent<Collider2D>().bounds.center + new Vector3(GetComponent<Collider2D>().bounds.size.x / 2, 0.5f, 0f);

        if (Physics2D.OverlapCircle(leftWallCheckPoint, 0.2f, playerDatabase.wallLayer))
            isLeftWall = true;
        else
            isLeftWall = false;

        if (Physics2D.OverlapCircle(rightWallCheckPoint, 0.2f, playerDatabase.wallLayer))
            isRightWall = true;
        else
            isRightWall = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundCheckPoint, 0.1f);
        Gizmos.DrawWireSphere(leftWallCheckPoint, 0.2f);
        Gizmos.DrawWireSphere(rightWallCheckPoint, 0.2f);
    }
}

