using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    GameObject oneWayPlatform;
    Collider2D playerColi;
    string ONEWAYPLATFORM = "OneWayPlatform";
    void Start()
    {
        playerColi = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
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
        Physics2D.IgnoreCollision(playerColi, platformCollider);

        yield return new WaitForSeconds(01f);

        Physics2D.IgnoreCollision(playerColi, platformCollider, false);
    }
}
