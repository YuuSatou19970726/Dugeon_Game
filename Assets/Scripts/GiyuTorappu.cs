using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiyuTorappu : MonoBehaviour
{
    CultistBlueMagician cultistBlueMagician;

    private void Awake()
    {
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cultistBlueMagician.SetIsFallingWater(true);
            Destroy(gameObject);
        }
    }
}
