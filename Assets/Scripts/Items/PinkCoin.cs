using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkCoin : MonoBehaviour
{
    MainGame mainGame;

    [SerializeField]
    LayerMask jumpableGround;

    BoxCollider2D boxCollider2D;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (IsGrounded())
        {
            gameObject.SetActive(false);
        } else
        {
            CointMovement();
        }
    }

    void CointMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = Random.Range(8f, 10f);
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
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
