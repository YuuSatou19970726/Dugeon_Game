using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    #region VARIABLES DECLARE
    EnemyDatabase enemyDatabase;

    [Header("Miscellaneous")]
    int originSpriteDirection;
    [HideInInspector] public int faceDirection;
    Rigidbody2D rigid;
    GameObject healthBar;
    GameObject healthBarHolder;
    float maxHealth;
    [HideInInspector] public float health;
    float healthRate;
    public bool isHurt;

    [Header("Movement")]
    float moveSpeed;

    [Header("Attack")]
    LayerMask guardLayer;
    float minAttackRange;
    float maxAttackRange;
    [HideInInspector] public Collider2D playerColi;
    public bool playerInRange;
    public bool canAttack;
    public bool playerDied;

    [Header("Guard and Patrol")]
    public bool playerDetected;
    float detectRange;
    [HideInInspector] public float idleTime;
    [HideInInspector] public Transform target;
    Transform[] patrolPoints = new Transform[2];
    int index = 0;
    float dieDelayTime;
    public bool isDied = false;
    #endregion

    #region MONOBEHAVIOUS
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemyDatabase = GetComponent<EnemyDatabase>();
    }
    void Start()
    {
        GetEnemyData();
        SetHealth();
    }
    void Update()
    {
        FindPlayer();

        DisplayHealth();

        if (Input.GetKeyDown(KeyCode.T)) GetDamage(10);
    }
    #endregion


    #region DATA GETTER

    void GetEnemyData()
    {
        originSpriteDirection = enemyDatabase.originSpriteDirection;

        healthBar = enemyDatabase.healthBar;
        healthBarHolder = enemyDatabase.healthBarHolder;

        maxHealth = enemyDatabase.maxHealth;

        guardLayer = enemyDatabase.guardLayer;

        minAttackRange = enemyDatabase.minAttackRange;
        maxAttackRange = enemyDatabase.maxAttackRange;
        detectRange = enemyDatabase.detectRange;

        idleTime = enemyDatabase.idleTime;
        patrolPoints = enemyDatabase.patrolPoints;
        moveSpeed = enemyDatabase.moveSpeed;

        dieDelayTime = enemyDatabase.dieDelayTime;
    }
    #endregion


    #region FIND PLAYER
    public void FindPlayer()
    {
        playerColi = Physics2D.OverlapCircle(rigid.transform.position, detectRange, guardLayer);

        playerDetected = playerColi != null;

        if (playerColi)
            target = playerColi.transform;
        else
            target = patrolPoints[index];
    }
    #endregion

    #region MOVEMENT
    public void MoveToTarget(Vector3 target)
    {
        faceDirection = Mathf.RoundToInt(Mathf.Sign(target.x - rigid.transform.position.x)) * originSpriteDirection;

        rigid.transform.localScale = new Vector3(faceDirection, 1, 1);

        int moveDirection = GetMoveDirection(target);

        if (Vector2.Distance(transform.position, target) > 0.5f)

            rigid.velocity = new Vector3(moveSpeed * moveDirection, rigid.velocity.y, 0);

        else
        {
            index++;

            if (index >= patrolPoints.Length)
            {
                index = 0;
            }
        }
    }


    int GetMoveDirection(Vector3 target)
    {
        GetAttackRange();

        if (!playerDetected) return (rigid.transform.position.x < target.x) ? 1 : -1;

        if (Mathf.Abs(rigid.transform.position.x - target.x) < 0.5f) return 0;

        float distance = Vector2.Distance(rigid.transform.position, target);

        if (rigid.transform.position.x < target.x)
        {
            if (distance > maxAttackRange)
            {
                return 1;
            }
            else if (distance < minAttackRange)
            {
                return -1;
            }
        }
        else if (rigid.transform.position.x > target.x)
        {
            if (distance > maxAttackRange)
            {
                return -1;
            }
            else if (distance < minAttackRange)
            {
                return 1;
            }
        }

        return 0;
    }
    #endregion

    #region ATTACK
    Vector2 minAtkPoint;
    Vector2 maxAtkPoint;

    void GetAttackRange()
    {
        minAtkPoint = new Vector2(transform.position.x + minAttackRange * faceDirection * originSpriteDirection, transform.position.y);
        maxAtkPoint = new Vector2(transform.position.x + maxAttackRange * faceDirection * originSpriteDirection, transform.position.y);

        Collider2D hit = Physics2D.OverlapArea(minAtkPoint, maxAtkPoint, enemyDatabase.guardLayer);
        if (hit) playerInRange = true;
        else playerInRange = false;
    }
    public void Attack()
    {
        if (!canAttack) return;

        GetComponent<IAttacker>().Attack();
    }

    public void RunAttackCooldownCoroutine()
    {
        StartCoroutine(AttackCooldownCoroutine());
    }
    IEnumerator AttackCooldownCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyDatabase.attackCooldown);
        canAttack = true;
    }
    #endregion


    #region GET DAMAGES
    public void GetDamage(float damage)
    {
        health -= damage;

        isHurt = true;
    }
    public float GetHealth()
    {
        return health;
    }
    void SetHealth()
    {
        health = maxHealth;
    }

    void DisplayHealth()
    {
        if (health <= 0) health = 0;

        healthRate = health / maxHealth;

        healthBar.transform.localScale = new Vector3(healthRate, healthBar.transform.localScale.y, 0f);

        healthBarHolder.transform.position = new Vector3(transform.position.x, healthBarHolder.transform.position.y);
    }
    #endregion


    #region DEATH
    public void DieTimeDelay()
    {
        StartCoroutine(ObjectDie());
    }

    IEnumerator ObjectDie()
    {
        isDied = true;
        rigid.velocity = Vector2.zero;
        rigid.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(dieDelayTime);
        transform.parent.gameObject.SetActive(false);
    }

    public void ObjectRespawn(Vector2 position)
    {
        SetHealth();
        transform.position = position;
        GetComponent<Collider2D>().enabled = true;
        rigid.isKinematic = false;
        canAttack = true;
        isDied = false;
    }
    #endregion 

 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(minAtkPoint, maxAtkPoint);
    }
}
