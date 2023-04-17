using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public Transform pointA, pointB;
    // public float damage = 30;
    public LayerMask attackable;
    public bool isAttacking, isAttackPressed, canClick;
    Player_Animator player_Animator;
    int hitCount = 0;
    float timer = 0;
    float comboTimer = 0;
    const string ENEMY = "Enemy";
    int clickCount = -1;
    void Awake()
    {
        player_Animator = FindObjectOfType<Player_Animator>();
    }
    void Start()
    {
        canClick = true;
    }
    void Update()
    {
        timer += Time.deltaTime;
        comboTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            isAttackPressed = true;
            isAttacking = true;
            canClick = false;
        }

        if (isAttackPressed && isAttacking)
        {
            clickCount++;
            // Debug.Log(clickCount + " = " + timer);

            Attack(clickCount + 5);

            StartCoroutine("AnimationDelay");
            isAttackPressed = false;
        }

        if (clickCount >= 2) clickCount = -1;
        if (comboTimer > 5f) { comboTimer = 0; hitCount = 0; isAttacking = false; clickCount = -1; }
    }
    void Attack(int index)
    {
        player_Animator.AnimationPlay(index);


        Collider2D[] targets = Physics2D.OverlapAreaAll(pointA.position, pointB.position, attackable);

        foreach (Collider2D coli in targets)
        {
            if (coli.CompareTag(ENEMY))
            {
                // Destroy(coli.gameObject);
                hitCount++;
                comboTimer = 0;

                Debug.Log(coli + " + " + hitCount);
            }
        }
    }

    IEnumerator AnimationDelay()
    {
        canClick = false;
        yield return new WaitForSeconds(0.5f);
        player_Animator.AnimationPlay(0);
        canClick = true;
    }
    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = Vector3.Lerp(pointA.position, pointB.position, 0.5f);
        Vector3 size = new Vector3(pointA.position.x - pointB.position.x, pointA.position.y - pointB.position.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
    #endregion
}
