using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IStopAttack
{
    #region MISCELLANEOUS
    PlayerDatabase playerDatabase;

    [Header("Miscellaneous")]
    float maxHealth;
    public bool isHurt;
    public bool isDied;
    InputControllerNew inputController;
    Rigidbody2D rigid;
    Collider2D coli;
    Transform head;
    public bool StopAttack()
    {
        return isDied;
    }
    #endregion

    #region MONOBEHAVIOUR
    void Awake()
    {
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        rigid = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();
    }
    void Start()
    {
        GetPlayerDatabase();
    }

    void Update()
    {
        GroundCheck();
        WallCheck();
        EdgeCheck();
        CoyoteJumpCheck();
        JumpCut();
        JumpReset();

        FallGravityChange();

        LastWallTime();
        LastDashTime();
    }

    #endregion

    #region DATA GETTER
    void GetPlayerDatabase()
    {
        head = playerDatabase.head;

        maxHealth = playerDatabase.maxHealth;

        moveSpeed = playerDatabase.moveSpeed;
        jumpForce = playerDatabase.jumpForce;

        gravity = playerDatabase.gravity;
        jumpNumber = playerDatabase.jumpNumber;
        maxCoyoteTime = playerDatabase.maxCoyoteTime;
        maxFallVelocity = playerDatabase.maxFallVelocity;

        groundLayer = playerDatabase.groundLayer;
        wallLayer = playerDatabase.wallLayer;

        dashingCooldown = playerDatabase.dashingCooldown;
        dashingPower = playerDatabase.dashingPower;
        dashingTime = playerDatabase.dashingTime;
    }
    #endregion


    #region HORIZONTAL MOVEMENT
    [Header("Movement")]
    float moveSpeed;

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
    #endregion

    #region GROUND JUMP
    [Header("Ground Jump")]
    LayerMask groundLayer;
    float jumpForce;
    float maxCoyoteTime;
    float maxFallVelocity;
    Vector2 groundCheckPoint;
    [HideInInspector] public bool isGrounded;
    float lastGroundTime;
    int jumpNumber;
    public int jumpCount;
    float timer;
    public void Jump()
    {
        if (jumpCount <= 0) return;
        timer = 0;

        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        jumpCount--;
    }

    void JumpReset()
    {
        if (Input.GetKey(KeyCode.Space)) return;

        if (isGrounded) jumpCount = jumpNumber;
    }

    void JumpCut()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f) return;

        if (Input.GetKeyUp(KeyCode.Space))
            StartCoroutine(StopJump(0));
    }

    void CoyoteJumpCheck()
    {
        if (isGrounded)
            lastGroundTime = 0f;
        else lastGroundTime += Time.deltaTime;

        if (lastGroundTime <= maxCoyoteTime)
            isGrounded = true;
    }
    #endregion

    #region JUMP AND GRAVITY CONTROL
    float gravity;
    void FallGravityChange()
    {
        if (isGrounded)
        {
            rigid.gravityScale = gravity;
            return;
        }

        if (isLeftEdge || isRightEdge)
        {
            rigid.gravityScale = 0f;
            return;
        }

        if (isLeftWall || isRightWall)
        {
            rigid.gravityScale = gravity / 2;
            return;
        }

        if (rigid.velocity.y < 0)
            rigid.gravityScale = gravity * 2;


        rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Max(rigid.velocity.y, -maxFallVelocity));
    }
    IEnumerator StopJump(float time)
    {
        yield return new WaitForSeconds(time);

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
    }
    #endregion

    #region CROUCH
    public void EnterCrouch()
    {
        head.position = transform.position;
    }

    public void ExitCrouch()
    {
        head.position = transform.position + Vector3.up / 2;
    }
    #endregion

    #region WALL JUMP
    [Header("Wall Jump")]
    LayerMask wallLayer;
    Vector2 leftWallCheckPoint, rightWallCheckPoint;
    [HideInInspector] public bool isLeftWall, isRightWall;
    [SerializeField] int jumpDirection;

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

        rigid.velocity = new Vector2(jumpDirection * moveSpeed, jumpForce);
        wallTimer = 0;

        StartCoroutine(StopJump(0.1f));
    }

    public void WallSlide()
    {
        if (isLeftWall) transform.localScale = new Vector2(-1, 1);
        if (isRightWall) transform.localScale = new Vector2(1, 1);
    }
    float wallTimer;
    void LastWallTime()
    {
        wallTimer += Time.deltaTime;

        if (wallTimer > 0.4f) return;

        rigid.gravityScale = gravity / 3;
    }
    #endregion

    #region WALL EDGE GRAB AND CLIMB
    [Header("Wall grab and climb")]
    [HideInInspector] public Collider2D wallEdge;
    [HideInInspector] public Vector2 wallEdgePoint;
    [HideInInspector] public bool isLeftEdge, isRightEdge;
    Vector2 upperLeftWallCheckPoint, upperRightWallCheckPoint;
    Vector3 pos = Vector3.zero;
    bool isGrabbing;
    public void WallEdgeGrab()
    {
        rigid.velocity = Vector2.zero;
    }


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

    #endregion

    #region DASH
    [Header("Dash")]
    float dashingPower;
    float dashingCooldown;
    public float dashingTime;
    float dashTimer;

    public void Dash()
    {
        if (dashTimer < dashingCooldown) return;

        float dashX;

        if (inputController.inputXRaw != 0)
            dashX = inputController.inputXRaw;
        else
            dashX = transform.localScale.x;

        rigid.AddForce(new Vector2(dashX, inputController.inputYRaw) * dashingPower, ForceMode2D.Impulse);

        AfterImagePool.instance.DisplaySprite();

        dashTimer = 0;
    }
    void LastDashTime()
    {
        dashTimer += Time.deltaTime;
    }
    #endregion

    #region COLLISION DETECT
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
        leftWallCheckPoint = coli.bounds.center + new Vector3(-coli.bounds.size.x / 2, 1.2f, 0f);
        rightWallCheckPoint = coli.bounds.center + new Vector3(coli.bounds.size.x / 2, 1.2f, 0f);

        Collider2D leftHit = Physics2D.OverlapArea(leftWallCheckPoint, new Vector2(leftWallCheckPoint.x - 0.2f, leftWallCheckPoint.y), wallLayer);

        if (leftHit)
        {
            isLeftWall = true;
            wallEdge = leftHit;
            wallEdgePoint = wallEdge.bounds.center + new Vector3(wallEdge.bounds.size.x / 2, wallEdge.bounds.size.y / 2, 0);
        }
        else
        {
            isLeftWall = false;
        }

        Collider2D rightHit = Physics2D.OverlapArea(rightWallCheckPoint, new Vector2(rightWallCheckPoint.x + 0.2f, rightWallCheckPoint.y), wallLayer);

        if (rightHit)
        {
            isRightWall = true;
            wallEdge = rightHit;
            wallEdgePoint = wallEdge.bounds.center + new Vector3(-wallEdge.bounds.size.x / 2, wallEdge.bounds.size.y / 2, 0);
        }
        else
        {
            isRightWall = false;
        }
    }

    void EdgeCheck()
    {
        upperLeftWallCheckPoint = coli.bounds.center + new Vector3(-coli.bounds.size.x / 2, 1.25f, 0f);
        upperRightWallCheckPoint = coli.bounds.center + new Vector3(coli.bounds.size.x / 2, 1.25f, 0f);

        RaycastHit2D leftHit = Physics2D.Raycast(upperLeftWallCheckPoint, Vector2.left, 0.2f, wallLayer);

        if (isLeftWall && !leftHit.collider)
        {
            isLeftEdge = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            isLeftEdge = false;

        RaycastHit2D rightHit = Physics2D.Raycast(upperRightWallCheckPoint, Vector2.right, 0.2f, wallLayer);

        if (isRightWall && !rightHit.collider)
        {
            isRightEdge = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
            isRightEdge = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint, 0.1f);
        Gizmos.DrawLine(leftWallCheckPoint, new Vector2(leftWallCheckPoint.x - 0.2f, leftWallCheckPoint.y));
        Gizmos.DrawLine(rightWallCheckPoint, new Vector2(rightWallCheckPoint.x + 0.2f, rightWallCheckPoint.y));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(upperLeftWallCheckPoint, upperLeftWallCheckPoint + Vector2.left * 0.2f);
        Gizmos.DrawLine(upperRightWallCheckPoint, upperRightWallCheckPoint + Vector2.right * 0.2f);
    }
    #endregion

}
