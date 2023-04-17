using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public Transform weapon, hitBox;
    // public float damage = 30;
    public LayerMask attackable;
    public bool isAttacking, isAttackPressed, canClick;
    Player_Animator player_Animator;
    int hitCount = 0;
    float timer = 0;
    const string ENEMY = "Enemy";
    int clickCount = -1;
    void Awake()
    {
        player_Animator = FindObjectOfType<Player_Animator>();
    }
    void Start()
    {
        isAttackPressed = false;
        isAttacking = false;
        canClick = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            isAttackPressed = true;
            canClick = false;
            clickCount++;

            Attack(clickCount);
        }
        timer += Time.deltaTime;

        if (timer >= 3f) ComboReset();
        if (timer >= 1f || clickCount > 2) clickCount = -1;
        if (timer >= 0.3f) canClick = true;
    }
    void Attack(int index)
    {
        if (isAttackPressed)
        {
            Collider2D[] targets = Physics2D.OverlapAreaAll(weapon.position, hitBox.position, attackable);

            foreach (Collider2D coli in targets)
            {
                if (coli.CompareTag(ENEMY))
                {
                    // Destroy(coli.gameObject);

                    hitCount++;
                    timer = 0;
                    // Debug.Log(coli + " + " + hitCount);
                }
            }

            // attackDelay = player_Animator.animator.GetCurrentAnimatorStateInfo(0).length;
            // Invoke("AttackComplete", attackDelay);

            // isAttackPressed = false;
        }
    }
    void AttackComplete()
    {
        player_Animator.AnimationPlay(0);
    }
    void ComboReset()
    {
        timer = 0;
        hitCount = 0;
    }

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(weapon.position, hitBox.position);
    }
    #endregion
}
