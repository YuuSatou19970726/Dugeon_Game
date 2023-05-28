using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    public static PlayerOneWayPlatform instance;
    GameObject oneWayPlatform;
    InputControllerNew inputController;
    string ONEWAYPLATFORM = "OneWayPlatform";
    [SerializeField] float ignoreTime = 0.5f;
    void Awake()
    {
        instance = this;

        inputController = GetComponent<InputControllerNew>();
    }

    public void GetThroughOneWayPlatform()
    {
        if (inputController.inputY < 0 && inputController.isJumpPress)
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
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D headCollider = GetComponent<PlayerDatabase>().head.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        Physics2D.IgnoreCollision(headCollider, platformCollider);

        yield return new WaitForSeconds(ignoreTime);

        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        Physics2D.IgnoreCollision(headCollider, platformCollider, false);
    }
}
