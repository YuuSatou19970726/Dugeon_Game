using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    PlayerDatabase playerDatabase;
    [SerializeField] Vector3 groundCheckpoint, headCheckpoint;
    [SerializeField] Vector3 leftWallCheckpoint, rightWallCheckpoint;
    [SerializeField] Vector3 upperLeftWallCheckpoint, upperRightWallCheckpoint;
    public Collider2D ledgePoint;
    public bool isGrounded, isHead;
    public bool isLeftWall, isRightWall;
    public bool isLeftEdge, isRightEdge;
    Vector3 checkpoint;
    public float longLine = 0.3f;

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void Awake()
    {
        playerDatabase = GetComponent<PlayerDatabase>();
    }
    void Update()
    {
        GroundCheck();
        HeadCheck();

        LeftWallCheck();
        RightWallCheck();

        LeftLedgeCheck();
        RightLedgeCheck();
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void GroundCheck()
    {
        Vector3 checkpoint = transform.position + groundCheckpoint;
        Collider2D groundHit = Physics2D.OverlapCircle(checkpoint, 0.1f, playerDatabase.groundLayer);

        isGrounded = groundHit ? true : false;
    }

    /**************************************************************************************************************************************************/

    void HeadCheck()
    {
        Vector3 checkpointA = transform.position + headCheckpoint;
        Vector3 checkpointB = new Vector3(checkpointA.x, checkpointA.y + 1f);

        Collider2D headHit = Physics2D.OverlapArea(checkpointA, checkpointB, playerDatabase.groundLayer);

        isHead = headHit ? true : false;
    }

    /**************************************************************************************************************************************************/
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
        Vector3 leftCheckpointB = new Vector3(leftCheckpointA.x - longLine, leftCheckpointA.y);
        Gizmos.DrawLine(leftCheckpointA, leftCheckpointB);

        Vector3 rightCheckpointA = transform.position + rightWallCheckpoint;
        Vector3 rightCheckpointB = new Vector3(rightCheckpointA.x + longLine, rightCheckpointA.y);
        Gizmos.DrawLine(rightCheckpointA, rightCheckpointB);

        Gizmos.color = Color.green;

        Vector3 upLeftPointA = transform.position + upperLeftWallCheckpoint;
        Vector3 upLeftPointB = new Vector3(upLeftPointA.x - longLine, upLeftPointA.y);
        Gizmos.DrawLine(upLeftPointA, upLeftPointB);

        Vector3 upRightPointA = transform.position + upperRightWallCheckpoint;
        Vector3 upRightPointB = new Vector3(upRightPointA.x + longLine, upRightPointA.y);
        Gizmos.DrawLine(upRightPointA, upRightPointB);
    }


    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void LeftWallCheck()
    {
        Vector3 leftCheckpointA = transform.position + leftWallCheckpoint;
        Vector3 leftCheckpointB = new Vector3(leftCheckpointA.x - longLine, leftCheckpointA.y);

        Collider2D leftHit = Physics2D.OverlapArea(leftCheckpointA, leftCheckpointB, playerDatabase.wallLayer);

        if (leftHit)
        {
            isLeftWall = true;

            ledgePoint = leftHit;
        }
        else
        {
            isLeftWall = false;
        }
    }

    /**************************************************************************************************************************************************/

    void RightWallCheck()
    {
        Vector3 rightCheckpointA = transform.position + rightWallCheckpoint;
        Vector3 rightCheckpointB = new Vector3(rightCheckpointA.x + longLine, rightCheckpointA.y);

        Collider2D rightHit = Physics2D.OverlapArea(rightCheckpointA, rightCheckpointB, playerDatabase.wallLayer);

        if (rightHit)
        {
            isRightWall = true;

            ledgePoint = rightHit;
        }
        else
        {
            isRightWall = false;
        }
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/

    void LeftLedgeCheck()
    {
        if (!isLeftWall || isHead || Input.GetAxisRaw("Vertical") < 0)
        {
            isLeftEdge = false;
            return;
        }

        Vector3 upLeftPointA = transform.position + upperLeftWallCheckpoint;
        Vector3 upLeftPointB = new Vector3(upLeftPointA.x - longLine, upLeftPointA.y);

        Collider2D leftUpper = Physics2D.OverlapArea(upLeftPointA, upLeftPointB, playerDatabase.wallLayer);

        if (leftUpper) return;

        isLeftEdge = true;
    }

    /**************************************************************************************************************************************************/

    void RightLedgeCheck()
    {
        if (!isRightWall || isHead || Input.GetAxisRaw("Vertical") < 0)
        {
            isRightEdge = false;
            return;
        }

        Vector3 upRightPointA = transform.position + upperRightWallCheckpoint;
        Vector3 upRightPointB = new Vector3(upRightPointA.x + longLine, upRightPointA.y);

        Collider2D rightUpper = Physics2D.OverlapArea(upRightPointA, upRightPointB, playerDatabase.wallLayer);

        if (rightUpper) return;

        isRightEdge = true;
    }

    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/


}
