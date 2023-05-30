using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IblastBullet : MonoBehaviour
{

    private void Start()
    {
        transform.Rotate(0, -180, 0);
    }

    private void Update()
    {
        IblastBulletMovement();
    }

    void IblastBulletMovement()
    {
        Vector2 speedVector2 = transform.position;
        speedVector2.x -= 5f * Time.deltaTime;
        transform.position = speedVector2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
