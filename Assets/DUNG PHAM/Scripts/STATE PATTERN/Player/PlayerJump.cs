using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerDatabase playerDatabase;
    PlayerController playerController;
    PlayerCollisionDetector playerCollision;
    Rigidbody2D rigid;
    Collider2D coli;
    int jumpCount;
    float timer;
    float lastGroundTime;

    void Awake()
    {
        playerCollision = GetComponent<PlayerCollisionDetector>();
        playerController = GetComponent<PlayerController>();
        playerDatabase = GetComponent<PlayerDatabase>();
        rigid = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();
    }
    void Update()
    {

        CoyoteJumpCheck();
        JumpCut();
        JumpReset();
    }
    public void Jump()
    {
        if (jumpCount <= 0) return;
        timer = 0;

        rigid.velocity = new Vector2(rigid.velocity.x, playerDatabase.jumpForce);
        jumpCount--;
    }

    void JumpReset()
    {
        if (Input.GetKey(KeyCode.Space)) return;

        if (playerCollision.isGrounded) jumpCount = playerDatabase.jumpNumber;
    }
    public void StopJump(float time)
    {
        StartCoroutine(StopJumpCoroutine(time));
    }

    void JumpCut()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f) return;

        if (Input.GetKeyUp(KeyCode.Space))
            StartCoroutine(StopJumpCoroutine(0));
    }

    void CoyoteJumpCheck()
    {
        if (playerCollision.isGrounded)
            lastGroundTime = 0f;
        else lastGroundTime += Time.deltaTime;

        if (lastGroundTime <= playerDatabase.maxCoyoteTime)
            playerCollision.isGrounded = true;
    }

    IEnumerator StopJumpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        rigid.velocity = new Vector2(rigid.velocity.x, 0);
    }

}
