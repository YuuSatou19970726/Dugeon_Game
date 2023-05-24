using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwimming : MonoBehaviour
{
    PlayerController playerController;
    string WATER = "Water";
    bool isSwimming;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isSwimming)
        {
            playerController.playerRigid.gravityScale = 0.8f;

            playerController.playerRigid.AddForce(new Vector2(playerController.playerProperties.inputX * playerController.playerProperties.swimSpeed, playerController.playerProperties.inputY * playerController.playerProperties.swimSpeed), ForceMode2D.Force);
        }
    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.tag == (WATER))
            isSwimming = true;
    }
    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.CompareTag(WATER))
            isSwimming = false;
    }
}
