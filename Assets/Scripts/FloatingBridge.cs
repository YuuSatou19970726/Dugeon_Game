using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBridge : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigidbody2D.isKinematic = false;
        }
    }
}
