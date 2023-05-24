using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityZone : MonoBehaviour
{
    #region Variables Declared
    PlayerController playerController;
    string GRAVITY = "Finish";
    #endregion
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.tag == (GRAVITY))
            playerController.playerProperties.moveOnAir = true;
    }
    void OnTriggerExit2D(Collider2D coli)
    {
        // if (coli.CompareTag(GRAVITY))
        // playerController.playerProperties.moveOnAir = false;
    }
}
