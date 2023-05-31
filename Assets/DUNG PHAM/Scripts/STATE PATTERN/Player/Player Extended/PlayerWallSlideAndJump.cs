using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideAndJump : MonoBehaviour
{
    PlayerDatabase playerDatabase;
    PlayerController playerController;
    InputControllerNew inputController;
    PlayerCollisionDetector playerCollision;
    Rigidbody2D playerRigid;
    PlayerJump playerJump;
    public float wallTimer;
    [SerializeField] int jumpDirection;

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerJump = GetComponent<PlayerJump>();
        playerCollision = GetComponent<PlayerCollisionDetector>();
    }
    void Update()
    {
        if (playerCollision.isLeftWall)
        {
            jumpDirection = 1;
        }
        else if (playerCollision.isRightWall)
        {
            jumpDirection = -1;
        }

        wallTimer += Time.deltaTime;
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    public void WallJump()
    {
        playerRigid.velocity = new Vector2(jumpDirection * playerDatabase.moveSpeed, playerDatabase.jumpForce);

        wallTimer = 0;

        playerJump.StopJump(0.1f);
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    public void WallSlide()
    {
        transform.localScale = new Vector2(-jumpDirection, 1);
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/



}
