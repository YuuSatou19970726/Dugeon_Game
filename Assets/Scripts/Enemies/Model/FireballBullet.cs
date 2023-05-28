using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireballBullet : MonoBehaviour
{
    private void Start()
    {
        transform.Rotate(0, 0, -90);
    }

    private void Update()
    {
        FireBallMovement();
    }

    void FireBallMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = Random.Range(1f, 4f);
        speedVector2.y -= speedRandom * Time.deltaTime;
        transform.position = speedVector2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
