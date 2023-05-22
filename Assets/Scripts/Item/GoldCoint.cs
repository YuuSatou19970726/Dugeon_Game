using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoint : MonoBehaviour
{
    MainGame mainGame;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainGame.IncrementScore(100);
            Destroy(gameObject);
        }
    }
}
