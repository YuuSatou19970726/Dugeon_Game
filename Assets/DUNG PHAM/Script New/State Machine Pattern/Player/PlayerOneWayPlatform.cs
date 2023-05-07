using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    public static PlayerOneWayPlatform instance;
    GameObject oneWayPlatform;
    PlayerMovementController playerController;
    string ONEWAYPLATFORM = "OneWayPlatform";
    void Awake()
    {
        instance = this;

        playerController = GetComponent<PlayerMovementController>();
    }

    public void GetThroughOneWayPlatform()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            if (oneWayPlatform)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coli)
    {
        if (coli.gameObject.tag == ONEWAYPLATFORM)
        {
            oneWayPlatform = coli.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D coli)
    {
        if (coli.gameObject.tag == ONEWAYPLATFORM)
        {
            oneWayPlatform = null;
        }
    }

    IEnumerator DisableCollision()
    {
        Collider2D platformCollider = oneWayPlatform.GetComponent<Collider2D>();
        Collider2D playerCollider = playerController.GetComponent<Collider2D>();
        Collider2D headCollider = playerController.head.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        Physics2D.IgnoreCollision(headCollider, platformCollider);

        yield return new WaitForSeconds(01f);

        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        Physics2D.IgnoreCollision(headCollider, platformCollider, false);
    }
}
