using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    #region Variable Declare
    [Header("Input System")]
    public InputAction inputAction;

    [Header("Movement")]
    Rigidbody2D rigid;
    public float speed;
    public float jumpForce;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public float input;
    public float groundRadius = 0.1f;

    [Header("Preference")]
    public LayerMask ground_Layer;
    public Transform wallCheckPoint;
    Player_Animator player_Animator;
    #endregion
    void Awake()
    {
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player_Animator = FindObjectOfType<Player_Animator>();
        isJumping = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            isJumping = true;
        }
    }
    void FixedUpdate()
    {
        Movement();
        player_Animator.IsJumping();
    }
    void Movement()
    {
        input = Input.GetAxisRaw("Horizontal") * speed;
        rigid.velocity = new Vector2(input, rigid.velocity.y);

        if (input > 0)
        {
            rigid.transform.localScale = new Vector3(1, 1, 0);
        }
        else if (input < 0)
        {
            rigid.transform.localScale = new Vector3(-1, 1, 0);
        }

        if (isJumping)
        {
            rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }
        player_Animator.IsMoving();
    }

    #region Ground and Wall check
    public bool GroundCheck()
    {
        Collider2D coli = GetComponent<Collider2D>();
        Vector3 lowestPoint = coli.bounds.center - new Vector3(0f, coli.bounds.extents.y, 0f);

        if (Physics2D.OverlapCircle(lowestPoint, groundRadius, ground_Layer) && !GetComponent<Interact_Tile>().isSwimming)
        {
            return true;
        }
        else { return false; }
    }

    public bool WallCheck()
    {
        if (Physics2D.OverlapCircle(wallCheckPoint.position, 0.2f, ground_Layer)) { return true; }
        else { return false; }
    }
    #endregion

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Collider2D coli = GetComponent<Collider2D>();
        Vector3 lowestPoint = coli.bounds.center - new Vector3(0f, coli.bounds.extents.y, 0f);

        Gizmos.DrawWireSphere(lowestPoint, groundRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, 0.3f);
    }
    #endregion
}
