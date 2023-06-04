using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBattle : MonoBehaviour
{
    [SerializeField]
    GameObject blueMagician;

    MainGame mainGame;
    CultistBlueMagician cultistBlueMagician;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void Start()
    {
        if (mainGame.GetIsMovie())
        {
            InstallBlueMagician();
        }
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
