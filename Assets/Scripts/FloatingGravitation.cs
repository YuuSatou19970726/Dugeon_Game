using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingGravitation : MonoBehaviour
{

    float deplayDestroy = 2f;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, deplayDestroy);
        }
    }
}
