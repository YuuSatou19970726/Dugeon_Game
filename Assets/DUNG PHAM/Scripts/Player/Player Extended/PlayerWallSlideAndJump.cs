using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideAndJump : MonoBehaviour
{
    PlayerDatabase playerDatabase;
    PlayerController playerController;
    Rigidbody2D playerRigid;
    Collider2D playerColi;

    [SerializeField] int jumpDirection;
    public float wallTimer;

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerColi = GetComponent<Collider2D>();
    }
    void Update()
    {
        LeftWallCheck();
        RightWallCheck();

        wallTimer += Time.deltaTime;
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    public void WallJump()
    {
        playerRigid.velocity = new Vector2(jumpDirection * playerDatabase.moveSpeed, playerDatabase.jumpForce);
        
        wallTimer = 0;

        playerController.StopJump(0.1f);
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    public void WallSlide()
    {
        transform.localScale = new Vector2(-jumpDirection, 1);
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/



    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/
    void LeftWallCheck()
    {
        playerDatabase.leftWallCheckPoint = playerColi.bounds.center + new Vector3(-playerColi.bounds.size.x / 2, playerDatabase.wallCheckDentaY, 0f);


        RaycastHit2D leftHit = Physics2D.Raycast(playerDatabase.leftWallCheckPoint, Vector2.left, 0.2f, playerDatabase.wallLayer);

        if (leftHit)
        {
            playerDatabase.isLeftWall = true;
            jumpDirection = 1;
        }
        else
        {
            playerDatabase.isLeftWall = false;
        }
    }

    void RightWallCheck()
    {
        playerDatabase.rightWallCheckPoint = playerColi.bounds.center + new Vector3(playerColi.bounds.size.x / 2, playerDatabase.wallCheckDentaY, 0f);

        RaycastHit2D rightHit = Physics2D.Raycast(playerDatabase.rightWallCheckPoint, Vector2.right, 0.2f, playerDatabase.wallLayer);

        if (rightHit)
        {
            playerDatabase.isRightWall = true;
            jumpDirection = -1;
        }
        else
        {
            playerDatabase.isRightWall = false;
        }
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerDatabase.leftWallCheckPoint, new Vector2(playerDatabase.leftWallCheckPoint.x - 0.2f, playerDatabase.leftWallCheckPoint.y));
        Gizmos.DrawLine(playerDatabase.rightWallCheckPoint, new Vector2(playerDatabase.rightWallCheckPoint.x + 0.2f, playerDatabase.rightWallCheckPoint.y));
    }
}
