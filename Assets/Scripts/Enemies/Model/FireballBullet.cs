using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireballBullet : MonoBehaviour
{

    [SerializeField]
    LayerMask jumpableGround;

    BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        transform.Rotate(0, 0, -90);
    }

    private void Update()
    {
        if (IsGrounded())
        {
            gameObject.SetActive(false);
        } else
        {
            FireBallMovement();
        }
    }

    void FireBallMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = Random.Range(3f, 7f);
        speedVector2.y -= speedRandom * Time.deltaTime;
        transform.position = speedVector2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
