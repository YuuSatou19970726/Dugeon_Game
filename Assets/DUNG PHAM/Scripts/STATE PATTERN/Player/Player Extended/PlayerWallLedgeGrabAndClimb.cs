using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallLedgeGrabAndClimb : MonoBehaviour
{
    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    Rigidbody2D playerRigid;
    Collider2D playerColi;
    PlayerCollisionDetector playerCollision;
    int ledgeSide;
    public Vector2 wallEdgePoint;


    /**************************************************************************************************************************************************/

    void Awake()
    {
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerCollision = GetComponent<PlayerCollisionDetector>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerColi = GetComponent<Collider2D>();
    }

    /**************************************************************************************************************************************************/

    void Update()
    {
        if (playerCollision.isLeftEdge)
            ledgeSide = -1;
        else if (playerCollision.isRightEdge)
            ledgeSide = 1;
    }

    /**************************************************************************************************************************************************/

    #region COLLISION DETECT

    #endregion

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    #region LEDGE GRAB
    public void WallEdgeGrab()
    {
        playerRigid.velocity = Vector2.zero;
        transform.localScale = new Vector3(ledgeSide, 1, 1);
    }
    #endregion

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    #region WALL CLIMB
    public void WallClimb()
    {
        StartCoroutine(WallClimbDelay());
    }

    /**************************************************************************************************************************************************/

    IEnumerator WallClimbDelay()
    {
        // GetEdgePoint(ledgeSide);

        yield return new WaitForSeconds(0.5f);

        Vector3 position = new Vector3(
            wallEdgePoint.x + playerColi.bounds.size.x / 2 * transform.localScale.x,
            wallEdgePoint.y + playerColi.bounds.size.y / 2 + 0.46f,
            0);

        transform.position = position;
    }

    /**************************************************************************************************************************************************/
    // void GetEdgePoint(int ledgeSide)
    // {
    //     switch (ledgeSide)
    //     {
    //         case -1:
    //             {
    //                 Collider2D leftHit = playerCollision.ledgePoint;

    //                 wallEdgePoint = leftHit.bounds.center + new Vector3(leftHit.bounds.size.x / 2, leftHit.bounds.size.y / 2, 0);
    //                 break;
    //             }
    //         case 1:
    //             {
    //                 Collider2D rightHit = playerCollision.ledgePoint;

    //                 wallEdgePoint = rightHit.bounds.center + new Vector3(-rightHit.bounds.size.x / 2, rightHit.bounds.size.y / 2, 0);
    //                 break;
    //             }
    //     }
    // }
    #endregion
}
