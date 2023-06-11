using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    PlayerDatabase playerDatabase;
    PlayerController playerController;
    [SerializeField] Vector3 groundCheckpoint, headCheckpoint;
    [SerializeField] Vector3 leftWallCheckpoint, rightWallCheckpoint;
    [SerializeField] Vector3 upperLeftWallCheckpoint, upperRightWallCheckpoint;
    public Vector2 ledgePoint;
    public bool isGrounded, isHead;
    public bool isLeftWall, isRightWall;
    public bool isLeftEdge, isRightEdge;
    public float longLine = 0.3f;
    public float fixHeight = 0.2f;

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void Awake()
    {
        playerDatabase = GetComponent<PlayerDatabase>();
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        isGrounded = GroundCheck();
        isHead = HeadCheck();

        isLeftWall = LeftWallCheck();
        isRightWall = RightWallCheck();

        isLeftEdge = LeftLedgeCheck();
        isRightEdge = RightLedgeCheck();
    }

    /************************************************************* GROUND CHECK ***********************************************************************/
    /**************************************************************************************************************************************************/

    public bool GroundCheck()
    {
        Vector3 checkpoint = transform.position + groundCheckpoint;
        Collider2D groundHit = Physics2D.OverlapCircle(checkpoint, 0.1f, playerDatabase.groundLayer);

        return groundHit;
    }
    /**************************************************************************************************************************************************/

    public bool HeadCheck()
    {
        Vector3 checkpointA = transform.position + headCheckpoint;
        Vector3 checkpointB = new Vector3(checkpointA.x, checkpointA.y + 1f);

        Collider2D headHit = Physics2D.OverlapArea(checkpointA, checkpointB, playerDatabase.groundLayer);

        return headHit;
    }

    /**************************************************************** GIZMOS DRAW *********************************************************************/
    /**************************************************************************************************************************************************/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 groundPoint = transform.position + groundCheckpoint;
        Gizmos.DrawWireSphere(groundPoint, 0.1f);

        Vector3 headPointA = transform.position + headCheckpoint;
        Vector3 headPointB = new Vector3(headPointA.x, headPointA.y + 1f);
        Gizmos.DrawLine(headPointA, headPointB);

        Gizmos.color = Color.yellow;

        Vector3 leftCheckpointA = transform.position + leftWallCheckpoint;
        Vector3 leftCheckpointB = new Vector3(leftCheckpointA.x - longLine, leftCheckpointA.y - fixHeight);
        Gizmos.DrawLine(leftCheckpointA, leftCheckpointB);

        Vector3 rightCheckpointA = transform.position + rightWallCheckpoint;
        Vector3 rightCheckpointB = new Vector3(rightCheckpointA.x + longLine, rightCheckpointA.y - fixHeight);
        Gizmos.DrawLine(rightCheckpointA, rightCheckpointB);

        Gizmos.color = Color.green;

        Vector3 upLeftPointA = transform.position + upperLeftWallCheckpoint;
        Vector3 upLeftPointB = new Vector3(upLeftPointA.x - longLine, upLeftPointA.y + fixHeight);
        Gizmos.DrawLine(upLeftPointA, upLeftPointB);

        Vector3 upRightPointA = transform.position + upperRightWallCheckpoint;
        Vector3 upRightPointB = new Vector3(upRightPointA.x + longLine, upRightPointA.y + fixHeight);
        Gizmos.DrawLine(upRightPointA, upRightPointB);
    }


    /******************************************************************** WALL CHECK ******************************************************************/
    /**************************************************************************************************************************************************/

    public bool LeftWallCheck()
    {
        Vector3 leftCheckpointA = transform.position + leftWallCheckpoint;
        Vector3 leftCheckpointB = new Vector3(leftCheckpointA.x - longLine, leftCheckpointA.y - fixHeight);

        Collider2D leftHit = Physics2D.OverlapArea(leftCheckpointA, leftCheckpointB, playerDatabase.wallLayer);

        return leftHit;
    }

    /**************************************************************************************************************************************************/

    public bool RightWallCheck()
    {
        Vector3 rightCheckpointA = transform.position + rightWallCheckpoint;
        Vector3 rightCheckpointB = new Vector3(rightCheckpointA.x + longLine, rightCheckpointA.y - fixHeight);

        Collider2D rightHit = Physics2D.OverlapArea(rightCheckpointA, rightCheckpointB, playerDatabase.wallLayer);

        return rightHit;
    }

    /*************************************************************** LEDGE CHECK **********************************************************************/
    /**************************************************************************************************************************************************/

    public bool LeftLedgeCheck()
    {
        if (!isLeftWall || isHead || Input.GetAxisRaw("Vertical") < 0)
        {
            return false;
        }

        Vector3 upLeftPointA = transform.position + upperLeftWallCheckpoint;
        Vector3 upLeftPointB = new Vector3(upLeftPointA.x - longLine, upLeftPointA.y + fixHeight);

        Collider2D leftUpper = Physics2D.OverlapArea(upLeftPointA, upLeftPointB, playerDatabase.wallLayer);

        if (leftUpper)
            return false;

        GetLedgePoint(upLeftPointB, -1);

        return true;
    }

    /**************************************************************************************************************************************************/

    public bool RightLedgeCheck()
    {
        if (!isRightWall || isHead || Input.GetAxisRaw("Vertical") < 0)
        {
            return false;
        }

        Vector3 upRightPointA = transform.position + upperRightWallCheckpoint;
        Vector3 upRightPointB = new Vector3(upRightPointA.x + longLine, upRightPointA.y + fixHeight);

        Collider2D rightUpper = Physics2D.OverlapArea(upRightPointA, upRightPointB, playerDatabase.wallLayer);

        if (rightUpper)
            return false;

        GetLedgePoint(upRightPointB, 1);

        return true;
    }

    /**************************************************************************************************************************************************/
    public void GetLedgePoint(Vector2 point, int side)
    {
        RaycastHit2D hitX = Physics2D.Raycast(transform.position, Vector2.right * side, 1, playerDatabase.wallLayer);
        RaycastHit2D hitY = Physics2D.Raycast(point, Vector2.down, 1, playerDatabase.wallLayer);

        if ((hitX && !hitY) || (!hitX && hitY)) return;

        float posX = hitX.point.x;
        float posY = hitY.point.y;

        ledgePoint = new Vector2(posX, posY);
    }

    /**************************************************************************************************************************************************/
}
