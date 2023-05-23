using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBullet : MonoBehaviour
{
    float speed = 10f;

    private void Start()
    {
    }

    private void Update()
    {
        FireBallMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
    }

    void FireBallMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = speed + Random.Range(1f, 3f);
        speedVector2.x += speedRandom * Time.deltaTime;
        transform.position = speedVector2;
    }
}
