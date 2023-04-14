using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingGravitation : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.3f);
        }
    }
}
