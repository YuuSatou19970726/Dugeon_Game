using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallLedgeGrabAndClimb : MonoBehaviour
{
    PlayerController playerController;
    PlayerDatabase playerDatabase;
    Rigidbody2D playerRigid;
    Collider2D playerColi;
    public Vector2 wallEdgePoint;
    int ledgeSide;

    /**************************************************************************************************************************************************/

    void Awake()
    {
        playerDatabase = GetComponent<PlayerDatabase>();
        playerController = GetComponent<PlayerController>();
        playerRigid = GetComponent<Rigidbody2D>();
        playerColi = GetComponent<Collider2D>();
    }

    /**************************************************************************************************************************************************/

    void Update()
    {
        LeftLedgeCheck();
        RightLedgeCheck();
    }

    /**************************************************************************************************************************************************/

    #region COLLISION DETECT
    void LeftLedgeCheck()
    {
        playerDatabase.upperLeftWallCheckPoint = playerColi.bounds.center + new Vector3(-playerColi.bounds.size.x / 2, playerDatabase.upperWallCheckDentaY, 0f);

        if (!playerDatabase.isLeftWall || Input.GetAxisRaw("Vertical") < 0)
        {
            playerDatabase.isLeftEdge = false;
            return;
        }

        RaycastHit2D leftUpper = Physics2D.Raycast(playerDatabase.upperLeftWallCheckPoint, Vector2.left, 0.2f, playerDatabase.wallLayer);

        if (leftUpper.collider) return;

        playerDatabase.isLeftEdge = true;

        ledgeSide = -1;
    }

    /**************************************************************************************************************************************************/

    void RightLedgeCheck()
    {
        playerDatabase.upperRightWallCheckPoint = playerColi.bounds.center + new Vector3(playerColi.bounds.size.x / 2, playerDatabase.upperWallCheckDentaY, 0f);

        if (!playerDatabase.isRightWall || Input.GetAxisRaw("Vertical") < 0)
        {
            playerDatabase.isRightEdge = false;
            return;
        }

        RaycastHit2D rightUpper = Physics2D.Raycast(playerDatabase.upperRightWallCheckPoint, Vector2.right, 0.2f, playerDatabase.wallLayer);

        if (rightUpper.collider) return;

        playerDatabase.isRightEdge = true;

        ledgeSide = 1;
    }

    /**************************************************************************************************************************************************/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerDatabase.upperLeftWallCheckPoint, playerDatabase.upperLeftWallCheckPoint + Vector2.left * 0.2f);
        Gizmos.DrawLine(playerDatabase.upperRightWallCheckPoint, playerDatabase.upperRightWallCheckPoint + Vector2.right * 0.2f);
    }
    #endregion

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    #region LEDGE GRAB
    public void WallEdgeGrab()
    {
        if (playerDatabase.isLeftEdge || playerDatabase.isRightEdge)
        {
            playerRigid.velocity = Vector2.zero;
            playerRigid.gravityScale = 0;
            transform.localScale = new Vector3(ledgeSide, 1, 1);
        }
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
        GetEdgePoint(ledgeSide);

        yield return new WaitForSeconds(0.5f);

        Vector3 position = new Vector3(
            wallEdgePoint.x + playerColi.bounds.size.x / 2 * transform.localScale.x,
            wallEdgePoint.y + playerColi.bounds.size.y / 2 + 0.46f,
            0);

        transform.position = position;
    }

    /**************************************************************************************************************************************************/
    void GetEdgePoint(int ledgeSide)
    {
        switch (ledgeSide)
        {
            case -1:
                {
                    Collider2D leftHit = Physics2D.OverlapArea(
                                playerDatabase.leftWallCheckPoint,
                                new Vector2(playerDatabase.leftWallCheckPoint.x - 0.2f, playerDatabase.leftWallCheckPoint.y),
                                playerDatabase.wallLayer);

                    wallEdgePoint = leftHit.bounds.center + new Vector3(leftHit.bounds.size.x / 2, leftHit.bounds.size.y / 2, 0);
                    break;
                }
            case 1:
                {
                    Collider2D rightHit = Physics2D.OverlapArea(
                                playerDatabase.rightWallCheckPoint,
                                new Vector2(playerDatabase.rightWallCheckPoint.x + 0.2f, playerDatabase.rightWallCheckPoint.y),
                                playerDatabase.wallLayer);

                    wallEdgePoint = rightHit.bounds.center + new Vector3(-rightHit.bounds.size.x / 2, rightHit.bounds.size.y / 2, 0);
                    break;
                }
        }
    }
    #endregion
}
