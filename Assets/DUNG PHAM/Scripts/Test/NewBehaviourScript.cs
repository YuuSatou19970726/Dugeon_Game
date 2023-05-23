using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rigid;
    Collider2D coli;

    [Header("Basic Data")]
    public float runSpeed = 20f;
    public float jumpForce = 30f;
    public float gravity = 10f;
    public int jumpNumber = 1;
    public LayerMask groundLayer;

    [Header("Variables")]
    public float maxJumpHeight;
    public float maxJumpTime;
    public float lowJumpHeight;
    public float lowJumpTime;
    public int jumpCount;
    Vector2 groundCheckPoint;
    Vector2 lastGroundPostion;
    bool isGrounded;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();
    }
    void Start()
    {
        GetHeight();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Gravity();
        GroundCheck();
        JumpReset();
        JumpCut();

        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    void Run()
    {
        float inputX = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(inputX * runSpeed, rigid.velocity.y);
    }
    void Jump()
    {
        if (jumpCount <= 0) return;

        timer = 0;
        lastGroundPostion = transform.position;

        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        jumpCount--;
    }
    void JumpReset()
    {
        if (Input.GetKey(KeyCode.Space)) return;

        if (isGrounded) jumpCount = jumpNumber;
    }
    void Gravity()
    {

        if (rigid.velocity.y < 0) rigid.gravityScale = gravity * 2;
        else
            rigid.gravityScale = gravity;
    }
    void JumpCut()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f) return;

        if (Input.GetKeyUp(KeyCode.Space)) rigid.velocity = new Vector2(rigid.velocity.x, 0);
    }

    void GetHeight()
    {
        float gra = Mathf.Pow(gravity, 2);

        maxJumpHeight = Mathf.Pow(jumpForce, 2) / (2 * gra);

        maxJumpTime = Mathf.Sqrt(2 * maxJumpHeight / gra);

        lowJumpHeight = maxJumpHeight / 2;

        float a = -0.5f * gra;
        float b = jumpForce;
        float c = -lowJumpHeight;
        float delta = b * b - 4 * a * c;
        float sqrtDelta = Mathf.Sqrt(delta);
        float solution1 = (-b + sqrtDelta) / (2 * a);
        float solution2 = (-b - sqrtDelta) / (2 * a);

        lowJumpTime = Mathf.Min(solution1, solution2);
    }

    void GroundCheck()
    {
        groundCheckPoint = coli.bounds.center - new Vector3(0f, coli.bounds.size.y / 2, 0f);

        if (Physics2D.OverlapCircle(groundCheckPoint, 0.1f, groundLayer))
            isGrounded = true;
        else
            isGrounded = false;
    }
}
