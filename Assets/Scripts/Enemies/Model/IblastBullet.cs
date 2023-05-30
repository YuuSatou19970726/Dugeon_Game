using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IblastBullet : MonoBehaviour
{
    BaseCurrent baseCurrent;

    BoxCollider2D boxCollider2D;

    string currentState = "Iblast_Attack_Animation";
    //animator
    Animator animator;

    private void Start()
    {
        baseCurrent = gameObject.AddComponent<BaseCurrent>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
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
            boxCollider2D.isTrigger = false;
            ChangeAnimationState(baseCurrent.GetIblastExplosion());
            float animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("ObjectActive", animatorDeplay);
        }
    }

    void ObjectActive()
    {
        gameObject.SetActive(false);
        boxCollider2D.isTrigger = true;
        ChangeAnimationState(baseCurrent.GetIblastAttack());
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
