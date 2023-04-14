using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;

    float xDirection;

    bool isJumping = false;
    float jumpForce = 5f;

    [SerializeField]
    LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xDirection = Input.GetAxisRaw("Horizontal");
        float moveStep = 10f * xDirection * Time.deltaTime;
        transform.position = transform.position + new Vector3(moveStep, 0, 0);

        if(Input.GetButtonDown("Jump") && !isJumping && IsGrounded())
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
