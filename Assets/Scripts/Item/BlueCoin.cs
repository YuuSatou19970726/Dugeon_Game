using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueCoin : MonoBehaviour
{
    MainGame mainGame;

    [SerializeField]
    GameObject coinBlue;

    CultistBlueMagician cultistBlueMagician;

    float roundTime = 5f;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
        cultistBlueMagician = FindAnyObjectByType<CultistBlueMagician>();
    }

    private void Update()
    {
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            if (roundTime <= 0)
            {
                roundTime = 0;
                SpawnCoinBlue();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainGame.IncrementScore(50);
            cultistBlueMagician.IncrementCoin();
            SpawnCoinBlue();
            Destroy(gameObject);
        }
    }

    void SpawnCoinBlue()
    {
        if (cultistBlueMagician.GetIsFallingWater())
        {
            Vector2 vector2Position = new Vector2(Random.Range(14.5f, 26.5f), Random.Range(-1.5f, -0.3f));

            Instantiate(coinBlue, vector2Position, Quaternion.identity);
        }
    }
}
