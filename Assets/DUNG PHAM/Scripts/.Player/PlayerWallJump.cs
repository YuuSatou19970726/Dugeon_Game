using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    PlayerController playerController;
    Vector3 leftPoint;
    Vector3 rightPoint;
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckWall();
    }
    void FixedUpdate()
    {
        WallJump();
    }


    void CheckWall()
    {
        leftPoint = playerController.playerColi.bounds.center - new Vector3(playerController.playerColi.bounds.size.x / 2, -0.5f);

        if (Physics2D.OverlapCircle(leftPoint, 0.2f, playerController.playerProperties.wallLayer))
        {
            playerController.playerProperties.isLeftWall = true;
        }
        else { playerController.playerProperties.isLeftWall = false; }


        rightPoint = playerController.playerColi.bounds.center + new Vector3(playerController.playerColi.bounds.size.x / 2, 0.5f);

        if (Physics2D.OverlapCircle(rightPoint, 0.2f, playerController.playerProperties.wallLayer))
        {
            playerController.playerProperties.isRightWall = true;
        }
        else { playerController.playerProperties.isRightWall = false; }
    }

    void WallJump()
    {
        if (playerController.playerProperties.isGrounded) return;        

        if (!playerController.playerProperties.isWallJumping) return;

        int jumpWay = playerController.playerProperties.isLeftWall ? 1 : -1;

        playerController.playerRigid.AddForce(new Vector2(jumpWay * playerController.playerProperties.maxSpeed * 2, playerController.playerProperties.jumpForce), ForceMode2D.Impulse);

        playerController.playerProperties.isWallJumping = false;

        if (playerController.playerRigid.velocity.y > 1f)
        {
            playerController.playerAnimation.PlayAnimation(playerController.playerAnimation.WALL_JUMP);
        }

        transform.localScale = new Vector3(jumpWay, 1, 0);
    }
    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftPoint, 0.2f);
        Gizmos.DrawWireSphere(rightPoint, 0.2f);
    }
    #endregion
}
