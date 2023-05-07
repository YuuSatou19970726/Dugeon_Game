using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (playerController.playerProperties.isDead) return;

        playerController.playerProperties.inputX = Input.GetAxis("Horizontal");
        playerController.playerProperties.inputXRaw = Input.GetAxisRaw("Horizontal");
        playerController.playerProperties.isHorizontalPress = (playerController.playerProperties.inputX != 0) ? true : false;

        playerController.playerProperties.inputY = Input.GetAxis("Vertical");
        playerController.playerProperties.inputYRaw = Input.GetAxisRaw("Vertical");
        playerController.playerProperties.isVerticalPress = (playerController.playerProperties.inputY != 0) ? true : false;

        playerController.playerProperties.isJumpPress = (Input.GetKeyDown(KeyCode.Space)) ? true : false;

        playerController.playerProperties.isLeftMousePress = (Input.GetMouseButtonDown(0)) ? true : false;

        playerController.playerProperties.isRightMousePress = (Input.GetMouseButtonDown(1)) ? true : false;

    }
}
