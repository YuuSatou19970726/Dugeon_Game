using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    Vector3 lowPoint, leftPoint, rightPoint;
    Collider2D coli;
    Rigidbody2D rigid;
    bool isGrounded, isJumping, isLeftWall, isRightWall, isWallJumping;
    [HideInInspector] public bool moveOnAir;
    [SerializeField] float maxSpeed, jumpForce;
    float runSpeed;
    float accelerationRate = 0.5f;
    [SerializeField] LayerMask groundLayer;
    int direction;
    [HideInInspector] public string IDLE = "Idle";
    const string RUN = "Run";
    const string IDLE_RUN = "Idle To Run";
    const string RUN_IDLE = "Run To Idle";
    const string JUMP = "Jump";
    const string ONAIR = "On Air";
    const string FALL = "Fall";
    const string HURT = "Hurt";
    const string WALL_JUMP = "Wall Jump";
    const string WALL_SIDE = "Wall Side";
    [HideInInspector] public string[] ATTACK = { "Attack 1", "Attack 2", "Attack 3" };
    string DEATH = "Death";
    [HideInInspector] public Animator animator;
    public AnimatorStateInfo state;
    #endregion

    #region Monobehaviour
    void Awake()
    {
        coli = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        isJumping = isWallJumping = false;
        runSpeed = 0;
    }
    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        CheckGround();
        CheckWall();
        CheckJumpCondition();
    }
    void FixedUpdate()
    {
        Movement();
        GroundJump();
        WallJump();
        Falling();
    }
    #endregion

    #region Movement
    void Movement()
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (input > 0) { direction = 1; Acceleration(); }
        else if (input < 0) { direction = -1; Acceleration(); }
        else if (input == 0) { direction = 0; Decceleration(); }
        if (direction != 0)
            transform.localScale = new Vector3(direction, 1, 1);
        if (moveOnAir)
            rigid.velocity = new Vector2(direction * runSpeed, rigid.velocity.y);
    }
    void Acceleration()
    {
        if (runSpeed < maxSpeed)
        {
            runSpeed += accelerationRate;
            if (runSpeed < 1)
            {
                MoveAnim(IDLE_RUN);
            }
        }
        else
        {
            runSpeed = maxSpeed;
            MoveAnim(RUN);
        }
    }
    void Decceleration()
    {
        if (runSpeed > 0)
        {
            runSpeed -= accelerationRate;
            if (runSpeed < 4)
            {
                MoveAnim(RUN_IDLE);
            }
        }
        else
        {
            runSpeed = 0;
            MoveAnim(IDLE);
        }
    }
    void CheckJumpCondition()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isJumping = true;
                moveOnAir = true;
            }
            else
            {
                if (isLeftWall || isRightWall) isWallJumping = true;
                moveOnAir = false;
            }
        }
    }
    void GroundJump()
    {
        if (!isJumping) return;

        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        isJumping = false;

        if (rigid.velocity.y > 1f)
        {
            PlayAnimation(JUMP);
        }
    }
    void WallJump()
    {
        if (isGrounded) return;

        if (isLeftWall || isRightWall)
        {
            PlayAnimation(WALL_SIDE);
        }

        if (isWallJumping)
        {
            // float delta = wallChecker.position.x - transform.position.x;
            // if (delta > 0) { jumpWay = -1; } else { jumpWay = 1; }
            int jumpWay;

            if (isLeftWall) { jumpWay = 1; } else { jumpWay = -1; }
            rigid.AddForce(new Vector2(jumpWay * maxSpeed * 2, jumpForce), ForceMode2D.Impulse);

            isWallJumping = false;

            if (rigid.velocity.y > 1f)
            {
                PlayAnimation(WALL_JUMP);
            }

            transform.localScale = new Vector3(jumpWay, 1, 0);
        }
    }
    void Falling()
    {
        if (isGrounded || isLeftWall || isRightWall) return;

        if (rigid.velocity.y <= -1f)
        {
            PlayAnimation(FALL);
        }
        else if (Mathf.Abs(rigid.velocity.y) < 1f)
            PlayAnimation(ONAIR);
    }
    #endregion

    #region Check Environment
    void CheckGround()
    {
        lowPoint = coli.bounds.center - new Vector3(0f, coli.bounds.size.y / 2, 0f);

        if (Physics2D.OverlapCircle(lowPoint, 0.1f, groundLayer)) { isGrounded = true; moveOnAir = true; } else { isGrounded = false; }
    }
    void CheckWall()
    {
        leftPoint = coli.bounds.center - new Vector3(coli.bounds.size.x / 2, 0f);
        rightPoint = coli.bounds.center + new Vector3(coli.bounds.size.x / 2, 0f);

        if (Physics2D.OverlapCircle(leftPoint, 0.2f, groundLayer))
        {
            isLeftWall = true;
        }
        else { isLeftWall = false; }
        if (Physics2D.OverlapCircle(rightPoint, 0.2f, groundLayer))
        {
            isRightWall = true;
        }
        else { isRightWall = false; }
    }
    #endregion

    #region Animation
    public void PlayAnimation(string clip)
    {
        if (state.IsName(clip)) return;

        animator.CrossFade(clip, 0, 0);
    }
    public void MoveAnim(string clip)
    {
        if (state.IsName(ATTACK[0]) || state.IsName(ATTACK[1]) || state.IsName(ATTACK[2]) || state.IsName(HURT) || state.IsName(DEATH))
        {
            if (state.normalizedTime < 1) return;
        }
        if (isGrounded)
            animator.CrossFade(clip, 0);
    }
    #endregion

    #region Hurt and Die
    public void BeDamagedAnim()
    {
        PlayAnimation(HURT);

        MoveAnim(IDLE);
    }

    public IEnumerator BeDeath()
    {
        PlayAnimation(DEATH);

        yield return new WaitForSeconds(1f);
    }
    #endregion

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(leftPoint, 0.2f);
        Gizmos.DrawWireSphere(rightPoint, 0.2f);
        Gizmos.DrawWireSphere(lowPoint, 0.1f);
    }
    #endregion
}
