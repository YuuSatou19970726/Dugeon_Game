using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{

    [Header("Miscellaneous")]
    public int originSpriteDirection = -1;
    [HideInInspector] public int faceDirection;
    Rigidbody2D rigid;
    public GameObject healthBar;
    public GameObject healthBarHolder;
    public float maxHealth = 100f;
    [HideInInspector] public float health;
    float healthRate;
    public bool isHurt;

    [Header("Movement")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float moveSpeed = 3f;
    Vector3 groundCheckPoint;
    [SerializeField] bool isGrounded;

    [Header("Attack")]
    public LayerMask guardLayer;
    public LayerMask attackLayer;
    public float attackDamage = 5;
    public float attackSpeed = 0.1f;
    public float reloadTime = 5f;
    public float minAttackRange = 2;
    public float maxAttackRange = 5;
    [HideInInspector] public Collider2D playerColi;
    public bool playerInRange;
    public bool canAttack = true;
    public bool playerDied;

    [Header("Guard and Patrol")]
    [SerializeField] float detectRange = 7;
    public float idleTime = 2;
    public bool playerDetected;
    [HideInInspector] public Transform target;
    [SerializeField] Transform[] patrolPoints = new Transform[2];
    int index = 0;
    public float dieDelayTime = 3;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        SetHealth();
    }
    void Update()
    {
        CheckGround();
        FindPlayer();

        DisplayHealth();

        if (Input.GetKeyDown(KeyCode.T)) GetDamage(10);
    }
    public void FindPlayer()
    {
        playerColi = Physics2D.OverlapCircle(rigid.transform.position, detectRange, guardLayer);

        playerDetected = playerColi != null;

        if (playerColi)
            target = playerColi.transform;
        else
            target = patrolPoints[index];
    }

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
        playerInRange = false;

        if (!playerDetected) return (rigid.transform.position.x < target.x) ? 1 : -1;

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

        playerInRange = true;
        return 0;
    }

    void CheckGround()
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        groundCheckPoint = bounds.center - new Vector3(0, bounds.size.y / 2, 0);

        Collider2D ground = Physics2D.OverlapCircle(groundCheckPoint, 0.1f, groundLayer);

        isGrounded = ground ? true : false;
    }


    public void Attack()
    {
        if (!canAttack) return;

        GetComponent<IAttacker>().Attack(attackDamage);
        StartCoroutine(AttackCooldown());
    }
    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(reloadTime);
        canAttack = true;
    }
    public void GetDamage(float damage)
    {
        health -= damage;

        isHurt = true;
    }

    public void SetHealth()
    {
        health = maxHealth;
    }
    void DisplayHealth()
    {
        if (health <= 0) health = 0;

        healthRate = health / maxHealth;

        healthBar.transform.localScale = new Vector3(healthRate, healthBar.transform.localScale.y, 0f);

        float healthBarHeight = transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y;

        healthBarHolder.transform.position = new Vector3(transform.position.x - 0.5f, healthBarHeight);
    }

    public void DieTimeDelay()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        StartCoroutine(ObjectDie());
    }
    IEnumerator ObjectDie()
    {
        rigid.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(dieDelayTime);
        transform.parent.gameObject.SetActive(false);
    }

    public void ObjectRespawn(Vector2 position)
    {
        SetHealth();
        transform.position = position;
        rigid.isKinematic = false;
        GetComponent<Collider2D>().enabled = true;
    }

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint, 0.1f);
        Gizmos.DrawWireSphere(transform.position, minAttackRange);
        Gizmos.DrawWireSphere(transform.position, maxAttackRange);
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
    #endregion
}
