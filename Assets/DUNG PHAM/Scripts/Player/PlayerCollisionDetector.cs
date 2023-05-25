using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    PlayerDatabase playerDatabase;
    [SerializeField] Vector3 groundCheckPoint;
    [SerializeField] Vector3 leftWallCheckpoint, rightWallCheckpoint;
    [SerializeField] Vector3 upperLeftWallCheckpoint, upperRightWallCheckpoint;
    public Collider2D ledgePoint;
    public bool isGrounded;
    public bool isLeftWall, isRightWall;
    public bool isLeftEdge, isRightEdge;
    Vector3 checkpoint;

    void Awake()
    {
        playerDatabase = GetComponent<PlayerDatabase>();
    }
    void Update()
    {
        GroundCheck();

        LeftWallCheck();
        RightWallCheck();

        LeftLedgeCheck();
        RightLedgeCheck();
    }


    void GroundCheck()
    {
        Vector3 checkpoint = transform.position + groundCheckPoint;
        Collider2D groundHit = Physics2D.OverlapCircle(checkpoint, 0.1f, playerDatabase.groundLayer);

        isGrounded = groundHit ? true : false;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 checkpoint = transform.position + groundCheckPoint;
        Gizmos.DrawWireSphere(checkpoint, 0.1f);

        Gizmos.color = Color.yellow;

        Vector3 leftCheckpoint = transform.position + leftWallCheckpoint;
        Vector3 rightCheckpoint = transform.position + rightWallCheckpoint;

        Gizmos.DrawLine(leftCheckpoint, new Vector2(leftCheckpoint.x - 0.2f, leftCheckpoint.y));
        Gizmos.DrawLine(rightCheckpoint, new Vector2(rightCheckpoint.x + 0.2f, rightCheckpoint.y));

        Gizmos.color = Color.green;

        Vector3 upLeftPoint = transform.position + upperLeftWallCheckpoint;
        Vector3 upRightPoint = transform.position + upperRightWallCheckpoint;

        Gizmos.DrawLine(upLeftPoint, upLeftPoint + Vector3.left * 0.2f);
        Gizmos.DrawLine(upRightPoint, upRightPoint + Vector3.right * 0.2f);
    }


    /**************************************************************************************************************************************************/
    /**************************************************************************************************************************************************/
    void LeftWallCheck()
    {
        Vector3 checkpoint = transform.position + leftWallCheckpoint;

        RaycastHit2D leftHit = Physics2D.Raycast(checkpoint, Vector2.left, 0.2f, playerDatabase.wallLayer);

        if (leftHit)
        {
            isLeftWall = true;

            ledgePoint = Physics2D.OverlapArea(checkpoint, new Vector2(checkpoint.x - 0.2f, checkpoint.y), playerDatabase.wallLayer);
        }
        else
        {
            isLeftWall = false;
        }
    }

    void RightWallCheck()
    {
        Vector3 checkpoint = transform.position + rightWallCheckpoint;

        RaycastHit2D rightHit = Physics2D.Raycast(checkpoint, Vector2.right, 0.2f, playerDatabase.wallLayer);

        if (rightHit)
        {
            isRightWall = true;

            ledgePoint = Physics2D.OverlapArea(checkpoint, new Vector2(checkpoint.x - 0.2f, checkpoint.y), playerDatabase.wallLayer);
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
        if (!isLeftWall || Input.GetAxisRaw("Vertical") < 0)
        {
            isLeftEdge = false;
            return;
        }

        Vector3 upLeftPoint = transform.position + upperLeftWallCheckpoint;
        RaycastHit2D leftUpper = Physics2D.Raycast(upLeftPoint, Vector2.left, 0.2f, playerDatabase.wallLayer);

        if (leftUpper.collider) return;

        isLeftEdge = true;
    }

    /**************************************************************************************************************************************************/

    void RightLedgeCheck()
    {
        if (!isRightWall || Input.GetAxisRaw("Vertical") < 0)
        {
            isRightEdge = false;
            return;
        }

        Vector3 upRightPoint = transform.position + upperRightWallCheckpoint;

        RaycastHit2D rightUpper = Physics2D.Raycast(upRightPoint, Vector2.right, 0.2f, playerDatabase.wallLayer);

        if (rightUpper.collider) return;

        isRightEdge = true;
    }

    /**************************************************************************************************************************************************/


}
