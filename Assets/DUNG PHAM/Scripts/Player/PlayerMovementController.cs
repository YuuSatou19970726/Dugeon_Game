using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IStopAttack
{
    [Header("Miscellaneous")]
    public float maxHealth = 100f;
    public bool isHurt;
    public bool isDied;
    InputControllerNew inputController;
    Rigidbody2D rigid;
    Collider2D coli;
    public Transform head;

    [Header("Movement")]
    public float moveSpeed = 10f;

    [Header("Ground Jump")]
    public LayerMask groundLayer;
    public float jumpForce = 10f;
    public float maxCoyoteTime = 0.1f;
    public float maxFallVelocity = 20f;
    Vector2 groundCheckPoint;
    [HideInInspector] public bool isGrounded;
    float lastGroundTime;

    [Header("Wall Jump")]
    public LayerMask wallLayer;
    public float jumpHeight = 4f;
    public float jumpDistance = 3f;
    public float timeJump = 0.7f;
    Vector2 leftWallCheckPoint, rightWallCheckPoint;
    [HideInInspector] public bool isLeftWall, isRightWall;


    Vector2 upperLeftWallCheckPoint, upperRightWallCheckPoint;
    [HideInInspector] public Collider2D wallEdge;
    [HideInInspector] public bool isLeftEdge, isRightEdge;
    [HideInInspector] public Vector2 wallEdgePoint;
    [SerializeField] int jumpDirection;

    [Header("Dash")]
    public float dashingPower = 30f;
    public float dashingTime = 0.5f;
    public float dashingCooldown = 1f;
    float dashTimer;



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
        rigid.velocity = new Vector2(moveSpeed * inputController.inputX, rigid.velocity.y);

        if (inputController.inputXRaw != 0) transform.localScale = new Vector2(inputController.inputXRaw, 1);
    }

    public void MoveOnAir()
    {
        rigid.velocity = new Vector2(moveSpeed * inputController.inputX, rigid.velocity.y);

        if (inputController.inputXRaw != 0) transform.localScale = new Vector2(inputController.inputXRaw, 1);
    }


    public void Jump()
    {
        rigid.velocity = Vector2.up * jumpForce;
    }

    void CoyoteJumpCheck()
    {
        if (isGrounded)
            lastGroundTime = 0f;
        else lastGroundTime += Time.deltaTime;

        if (lastGroundTime <= maxCoyoteTime)
            isGrounded = true;
    }

    void FallGravityChange()
    {
        if (isGrounded)
        {
            rigid.gravityScale = 1f;
            return;
        }
        if (!isGrounded)
        {
            if (isLeftWall || isRightWall)
            {
                rigid.gravityScale = 0.5f;
                return;
            }
        }


        if (inputController.isJumpHold)
        {
            if (rigid.velocity.y >= 0)
                rigid.gravityScale = 1f;

            else rigid.gravityScale = 7f;
        }
        else
        {
            rigid.gravityScale = 15f;
        }

        rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Max(rigid.velocity.y, -maxFallVelocity));
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

        Vector3 targetPosition = new Vector2(transform.position.x + jumpDistance * jumpDirection, transform.position.y + jumpHeight);

        StartCoroutine(MoveToTargetPosition(targetPosition));
    }

    IEnumerator MoveToTargetPosition(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startingPos = transform.position;

        while (elapsedTime < timeJump)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            transform.position = Vector2.Lerp(startingPos, targetPosition, elapsedTime);
            yield return null;
        }
    }

    public void Dash()
    {
        if (dashTimer < dashingCooldown) return;

        if (inputController.inputXRaw != 0)
            rigid.AddForce(new Vector2(inputController.inputXRaw, inputController.inputYRaw) * dashingPower, ForceMode2D.Impulse);
        else
            rigid.AddForce(new Vector2(transform.localScale.x * dashingPower, 0f), ForceMode2D.Impulse);

        AfterImagePool.instance.DisplaySprite();

        dashTimer = 0;
    }

    void GroundCheck()
    {
        groundCheckPoint = coli.bounds.center - new Vector3(0f, coli.bounds.size.y / 2, 0f);

        if (Physics2D.OverlapCircle(groundCheckPoint, 0.1f, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
    }
    void WallCheck()
    {
        leftWallCheckPoint = coli.bounds.center + new Vector3(-coli.bounds.size.x / 2, 0.5f, 0f);
        rightWallCheckPoint = coli.bounds.center + new Vector3(coli.bounds.size.x / 2, 0.5f, 0f);

        RaycastHit2D leftHit = Physics2D.Raycast(leftWallCheckPoint, Vector2.left, 0.2f, wallLayer);

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

        RaycastHit2D rightHit = Physics2D.Raycast(rightWallCheckPoint, Vector2.right, 0.2f, wallLayer);

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

        if (isLeftWall && !Physics2D.Raycast(upperLeftWallCheckPoint, Vector2.left, 0.2f, wallLayer))
        {
            isLeftEdge = true;
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else
            isLeftEdge = false;

        if (isRightWall && !Physics2D.Raycast(upperRightWallCheckPoint, Vector2.right, 0.2f, wallLayer))
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

    public bool StopAttack()
    {
        return isDied;
    }
}

