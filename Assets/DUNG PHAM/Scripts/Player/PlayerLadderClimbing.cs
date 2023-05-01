using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderClimbing : MonoBehaviour
{
    #region Variables Declared
    PlayerController playerController;
    bool isClimbing;
    string LADDER = "Ladder";
    #endregion
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (isClimbing)
        {
            playerController.playerRigid.gravityScale = 0f;

            playerController.playerRigid.velocity = new Vector3(playerController.playerRigid.velocity.x, Input.GetAxisRaw("Vertical") * playerController.playerProperties.climbSpeed, 0);
        }
        else
        {
            playerController.playerRigid.gravityScale = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.tag == (LADDER))
        {
            isClimbing = true;
            // playerController.playerProperties.moveOnAir = true;
        }
        // else isClimbing = false;
    }
    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.CompareTag(LADDER))
        {
            isClimbing = false;
            // playerController.playerProperties.moveOnAir = false;
        }
    }
}
