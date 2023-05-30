using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireballBullet : MonoBehaviour
{
    BaseCurrent baseCurrent;

    [SerializeField]
    LayerMask jumpableGround;

    BoxCollider2D boxCollider2D;

    string currentState = "Fire_Ball_Animation";
    //animator
    Animator animator;

    private void Start()
    {
        baseCurrent = gameObject.AddComponent<BaseCurrent>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        transform.Rotate(0, 0, -90);
    }

    private void Update()
    {
        if (IsGrounded())
        {
            if (currentState != baseCurrent.GetFireBallExplosion())
            {
                ChangeAnimationState(baseCurrent.GetFireBallExplosion());
                animator.transform.Rotate(0, 0, 90);
                float animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
                Invoke("ObjectActive", animatorDeplay);
            }
        } else
        {
            FireBallMovement();
        }
    }

    void ObjectActive()
    {
        gameObject.SetActive(false);
        ChangeAnimationState(baseCurrent.GetFireBallAttack());
        animator.transform.Rotate(0, 0, -90);
    }

    void FireBallMovement()
    {
        Vector2 speedVector2 = transform.position;
        float speedRandom = Random.Range(7f, 11f);
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

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
