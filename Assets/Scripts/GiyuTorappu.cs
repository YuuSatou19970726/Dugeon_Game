using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiyuTorappu : MonoBehaviour
{
    [SerializeField]
    GameObject blueMagician;

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
            InstallBlueMagician();
            Destroy(gameObject);
        }
    }

    void InstallBlueMagician()
    {
        Vector2 position = new Vector2(20f, -5.3f);
        Instantiate(blueMagician, position, Quaternion.identity);
    }
}
