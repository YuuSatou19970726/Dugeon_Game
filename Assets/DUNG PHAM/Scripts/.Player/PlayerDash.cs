using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    PlayerController playerController;
    public bool isDashing;
    public bool canDash = true;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (playerController.playerProperties.isGrounded) return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        playerController.playerProperties.moveOnAir = false;
        float originalGravity = playerController.playerRigid.gravityScale;
        playerController.playerRigid.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        playerController.playerRigid.gravityScale = originalGravity;
        isDashing = false;
        playerController.playerProperties.moveOnAir = true;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;

    }
}
