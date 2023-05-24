using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{

    InputControllerNew inputController;
    PlayerDatabase playerDatabase;
    Rigidbody2D rigid;
    [SerializeField] AfterImagePool afterImage;
    float dashTimer;

    void Awake()
    {
        inputController = GetComponent<InputControllerNew>();
        playerDatabase = GetComponent<PlayerDatabase>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        LastDashTime();
    }
    public void Dash()
    {
        if (dashTimer < playerDatabase.dashingCooldown) return;

        float dashX;

        if (inputController.inputXRaw != 0)
            dashX = inputController.inputXRaw;
        else
            dashX = transform.localScale.x;

        rigid.AddForce(new Vector2(dashX, inputController.inputYRaw) * playerDatabase.dashingPower, ForceMode2D.Impulse);

        afterImage.DisplaySprite();

        dashTimer = 0;
    }
    void LastDashTime()
    {
        dashTimer += Time.deltaTime;
    }
}
