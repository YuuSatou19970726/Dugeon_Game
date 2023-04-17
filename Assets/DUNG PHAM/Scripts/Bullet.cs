using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.tag == "Player") Destroy(gameObject);
        if (coli.gameObject.tag == "Ground") Destroy(gameObject);
    }
}
