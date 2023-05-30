using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBullet : MonoBehaviour
{
    int countLoop = 2;

    private void Update()
    {
        if (countLoop == 0)
        {
            gameObject.SetActive(false);
            countLoop = 2;
        } else
        {
            SparkBulletMovement();
        }
    }

    void SparkBulletMovement()
    {
        Vector2 speedVector2 = transform.position;
        speedVector2.x += 5f * Time.deltaTime;
        transform.position = speedVector2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            countLoop--;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            countLoop = 0;
        }
    }
}
