using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWarriorController : MonoBehaviour
{
    BaseCurrent baseCurrent;

    MainGame mainGame;

    EnemyPatrol enemyPatrol;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    float rangeCharacter = 5f;
    float colliderDistance = 0.75f;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    BoxCollider2D boxCollider2DCallGate;

    //Animation States
    string currentState = "Dragon_Warrior_Idle_Animation";

    //animator
    Animator animator;
    float animatorDeplay = 0.3f;

    [SerializeField]
    GameObject waypoint;
    float deplayBullet = 0f;
    int countFire = -1;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Start is called before the first frame update
    void Start()
    {
        baseCurrent = gameObject.AddComponent<BaseCurrent>();

        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInSight() && currentState != baseCurrent.GetDragonWarriorIdle() && currentState != baseCurrent.GetDragonWarriorAttack() && currentState != baseCurrent.GetDragonWarriorDie())
        {
            ChangeAnimationState(baseCurrent.GetDragonWarriorAttack());
            mainGame.SetCountGates();
            countFire = 5;
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

        if (countFire > 0)
        {
            if (deplayBullet >= 0)
                deplayBullet -= Time.deltaTime;

            if(deplayBullet <= 0)
                Fire();
        } else if (countFire == 0 && currentState == baseCurrent.GetDragonWarriorAttack())
        {
            ChangeAnimationState(baseCurrent.GetDragonWarriorIdle());
        }
    }

    void AnimationDeath()
    {
        ChangeAnimationState(baseCurrent.GetDragonWarriorDie());
        float animatorDeplay = animator.GetCurrentAnimatorStateInfo(0).length + 0.3f;
        Invoke("FireBallBattle", animatorDeplay);
    }

    void Fire()
    {
        GameObject bullet = ObjectPool.instance.GetPooledIblastBullet();

        Vector2 bodyPosition = waypoint.transform.position;

        if (bullet != null)
        {
            bullet.transform.position = bodyPosition;
            bullet.SetActive(true);
        }
        countFire--;
        deplayBullet = 0.5f;
    }

    void FireBallBattle()
    {
        mainGame.ActiveFireBall();
        gameObject.SetActive(false);
    }

    bool PlayerInSight()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2DCallGate.bounds.center + transform.right * rangeCharacter * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider2DCallGate.bounds.size.x * rangeCharacter, boxCollider2DCallGate.bounds.size.y, boxCollider2DCallGate.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        return raycastHit2D.collider != null;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(boxCollider2DCallGate.bounds.center + transform.right * rangeCharacter * transform.localScale.x * colliderDistance,
        //    new Vector3(boxCollider2DCallGate.bounds.size.x * rangeCharacter, boxCollider2DCallGate.bounds.size.y, boxCollider2DCallGate.bounds.size.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AnimationDeath();
        }
    }

    public void GetPlayerIdle()
    {
        if (currentState != baseCurrent.GetDragonWarriorDie())
        ChangeAnimationState(baseCurrent.GetDragonWarriorIdle());
    }

    public void GetDragonWarriorMove()
    {
        if (currentState != baseCurrent.GetDragonWarriorDie())
            ChangeAnimationState(baseCurrent.GetDragonWarriorMove());
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
