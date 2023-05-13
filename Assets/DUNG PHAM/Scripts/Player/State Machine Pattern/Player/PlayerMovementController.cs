using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public PlayerDatabase playerDatabase;
    InputControllerNew inputController;
    Rigidbody2D rigid;
    Collider2D coli;
    Vector2 groundCheckPoint;
    Vector2 leftWallCheckPoint, rightWallCheckPoint;
    Vector2 upperLeftWallCheckPoint, upperRightWallCheckPoint;
    public Transform head;
    public Collider2D wallEdge;
    public bool isGrounded;
    public bool isLeftWall, isRightWall;
    public bool isLeftEdge, isRightEdge;
    public Vector2 wallEdgePoint;
    float lastGroundTime;
    [SerializeField] int jumpDirection;
    float dashTimer;
    // public Vector3 grabPosition;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputControllerNew>();
        coli = GetComponent<Collider2D>();
    }

    void Update()
    {
        GroundCheck();
        WallCheck();
        EdgeCheck();
        CoyoteJumpCheck();

        FallGravityChange();

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

    void CoyoteJumpCheck()
    {
        if (isGrounded)
            lastGroundTime = 0f;
        else lastGroundTime += Time.deltaTime;

        if (lastGroundTime <= playerDatabase.maxCoyoteTime)
            isGrounded = true;
    }

    void FallGravityChange()
    {
        if (!isGrounded)
        {
            if (isLeftWall || isRightWall)
            {
                rigid.gravityScale = 0.5f;
                return;
            }
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

        if (rigid.velocity.y < -playerDatabase.maxFallVelocity)
            rigid.velocity = new Vector2(rigid.velocity.x, -playerDatabase.maxFallVelocity);
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
        groundCheckPoint = coli.bounds.center - new Vector3(0f, coli.bounds.size.y / 2, 0f);

        if (Physics2D.OverlapCircle(groundCheckPoint, 0.1f, playerDatabase.groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
    }
    void WallCheck()
    {
        leftWallCheckPoint = coli.bounds.center + new Vector3(-coli.bounds.size.x / 2, 0.5f, 0f);
        rightWallCheckPoint = coli.bounds.center + new Vector3(coli.bounds.size.x / 2, 0.5f, 0f);

        RaycastHit2D leftHit = Physics2D.Raycast(leftWallCheckPoint, Vector2.left, 0.2f, playerDatabase.wallLayer);

        if (leftHit)
        {
            isLeftWall = true;
            wallEdge = leftHit.collider;
            wallEdgePoint = wallEdge.bounds.center + new Vector3(wallEdge.bounds.size.x / 2, wallEdge.bounds.size.y / 2, 0);
        }
        else
        {
            isLeftWall = false;
        }

        RaycastHit2D rightHit = Physics2D.Raycast(rightWallCheckPoint, Vector2.right, 0.2f, playerDatabase.wallLayer);

        if (rightHit)
        {
            isRightWall = true;
            wallEdge = rightHit.collider;
            wallEdgePoint = wallEdge.bounds.center + new Vector3(-wallEdge.bounds.size.x / 2, wallEdge.bounds.size.y / 2, 0);
        }
        else
        {
            isRightWall = false;
        }
    }

    void EdgeCheck()
    {
        upperLeftWallCheckPoint = coli.bounds.center + new Vector3(-coli.bounds.size.x / 2, 1.2f, 0f);
        upperRightWallCheckPoint = coli.bounds.center + new Vector3(coli.bounds.size.x / 2, 1.2f, 0f);

        if (isLeftWall && !Physics2D.Raycast(upperLeftWallCheckPoint, Vector2.left, 0.2f, playerDatabase.wallLayer))
        {
            isLeftEdge = true;
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else
            isLeftEdge = false;

        if (isRightWall && !Physics2D.Raycast(upperRightWallCheckPoint, Vector2.right, 0.2f, playerDatabase.wallLayer))
        {
            isRightEdge = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
            isRightEdge = false;
    }

    public void WallEdgeGrab()
    {
        Vector3 position = wallEdgePoint - new Vector2((coli.bounds.size.x / 2 + 0.1f) * transform.localScale.x, coli.bounds.size.y / 2 + 0.15f);

        transform.position = position;
    }

    Vector3 pos = Vector3.zero;

    public void WallClimb()
    {
        StartCoroutine(WallClimbDelay());
    }
    IEnumerator WallClimbDelay()
    {
        if (pos == Vector3.zero)
            pos = transform.position;

        transform.position = pos;

        yield return new WaitForSeconds(0.5f);

        Vector3 position = new Vector3(wallEdgePoint.x + coli.bounds.size.x / 2 * transform.localScale.x, wallEdgePoint.y + coli.bounds.size.y / 2 + 0.46f, 0);

        transform.position = position;

        pos = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint, 0.1f);
        //     Gizmos.DrawWireSphere(leftWallCheckPoint, 0.2f);
        //     Gizmos.DrawWireSphere(rightWallCheckPoint, 0.2f);
        Gizmos.DrawLine(leftWallCheckPoint, leftWallCheckPoint + Vector2.left * 0.2f);
        Gizmos.DrawLine(rightWallCheckPoint, rightWallCheckPoint + Vector2.right * 0.2f);
        Gizmos.DrawLine(upperLeftWallCheckPoint, upperLeftWallCheckPoint + Vector2.left * 0.2f);
        Gizmos.DrawLine(upperRightWallCheckPoint, upperRightWallCheckPoint + Vector2.right * 0.2f);
    }
}

