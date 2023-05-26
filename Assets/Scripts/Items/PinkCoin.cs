using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkCoin : MonoBehaviour
{
    MainGame mainGame;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    private void Update()
    {
        CointMovement();
    }

    void CointMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = Random.Range(1f, 5f);
        speedVector2.y -= speedRandom * Time.deltaTime;
        transform.position = speedVector2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainGame.DecreaseCountOpenGate();
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
