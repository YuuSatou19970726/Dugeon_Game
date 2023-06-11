using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : IState
{
    Collider2D playerColi;
    Rigidbody2D playerRigid;
    PlayerStateManager player;
    PlayerCollisionDetector playerCollision;

    public PlayerWallClimbState(PlayerStateManager player)
    {
        this.player = player;
        this.playerCollision = player.playerCollision;
        playerRigid = player.GetComponent<Rigidbody2D>();
        playerColi = player.GetComponent<Collider2D>();
    }



    public void EnterState()
    {
        player.playerAnimation.PlayAnimatorClip(player.playerDatabase.WALLCLB);

        WallClimb();
    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {
    }

    public void UpdateState()
    {
        if (player.playerDatabase.isHurt)
            player.SwitchState(player.hurtState);

        if (player.playerDatabase.isDied)
            player.SwitchState(player.dieState);

        if (player.playerAnimation.currentState.IsName(player.playerDatabase.WALLCLB)
         && player.playerAnimation.currentState.normalizedTime >= 0.9f)
        {
            player.SwitchState(player.idleState);
        }
    }

    public void WallClimb()
    {
        player.StartCoroutine(WallClimbDelay());
    }

    /**************************************************************************************************************************************************/

    IEnumerator WallClimbDelay()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 position = new Vector3(
            playerCollision.ledgePoint.x + playerColi.bounds.size.x / 2 * player.transform.localScale.x,
            playerCollision.ledgePoint.y + playerColi.bounds.size.y / 2 + 0.46f,
            0);

        player.transform.position = position;
    }

    /**************************************************************************************************************************************************/
}
