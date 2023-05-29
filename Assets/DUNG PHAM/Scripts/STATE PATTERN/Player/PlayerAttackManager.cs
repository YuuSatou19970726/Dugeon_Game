using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour, IDamageable
{
    PlayerController playerController;
    PlayerDatabase playerDatabase;
    public List<Collider2D> attackColliders = new List<Collider2D>();
    public ContactFilter2D filter;
    Collider2D[] target = new Collider2D[5];
    Rigidbody2D rigid;
    [SerializeField] GameObject bloodPrefab;
    float health;
    int hitCount = 0;
    float hitTimer;
    public float hurtTimer;

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerDatabase = GetComponent<PlayerDatabase>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        playerDatabase.healthBar.ShowMaxHealth(playerDatabase.maxHealth);
        playerDatabase.healthBar.ShowHealth(health);

        playerDatabase.isHurt = false;
        playerDatabase.isDied = false;

        InitBloodEffect(20);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) GetDamage(10, transform);

        hitTimer += Time.deltaTime;
        hurtTimer += Time.deltaTime;

        BloodEffectReset();
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    public void AttackCast(int id)
    {
        hitReset();

        int count = Physics2D.OverlapCollider(attackColliders[id], filter, target);

        if (count == 0) return;

        for (int x = 0; x < count; x++)
        {
            hitCount += 1;

            target[x].GetComponent<IDamageable>().GetDamage(playerDatabase.attackDamage, attackColliders[id].transform);

            BloodSplit(target[x].transform.position, attackColliders[id].transform.position);

            // Recovery(1);
        }

        hitTimer = 0;
    }

    void hitReset()
    {
        if (hitTimer > playerDatabase.hitResetTime)
        {
            hitCount = 0;
        }
    }

    public void Recovery(float amount)
    {
        if (health >= playerDatabase.maxHealth)
        {
            health = playerDatabase.maxHealth;
            return;
        }

        health += amount;
        playerDatabase.healthBar.ShowHealth(health);
    }
    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    public void GetDamage(float damage, Transform knocker)
    {
        if (hurtTimer < 2f) return;

        hurtTimer = 0f;
        health -= damage;

        playerDatabase.isHurt = true;

        playerDatabase.healthBar.ShowHealth(health);

        BloodSplit(transform.position, knocker.position);
        BeKnockBack(knocker);
    }

    void BeKnockBack(Transform knocker)
    {
        Vector2 knockWay = transform.position - knocker.position;

        int knockX = knockWay.x < 0 ? -1 : 1;
        int knockY = knockWay.y < 0 ? -2 : 2;

        knockWay = new Vector2(knockX, knockY);

        rigid.velocity = knockWay * 5f;
    }

    int direction;

    void BloodSplit(Vector2 position, Vector2 knocker)
    {
        if (knocker.x < position.x)
            direction = -1;
        else
            direction = 1;

        Quaternion rotate = Quaternion.Euler(0, 0, 30 * direction);

        foreach (GameObject b in bloods)
        {
            if (b.activeInHierarchy) continue;

            b.transform.position = position;
            b.transform.rotation = rotate;
            b.SetActive(true);

            break;
        }
    }

    void BloodEffectReset()
    {
        foreach (GameObject b in bloods)
        {
            if (b.GetComponent<ParticleSystem>().isPlaying) return;

            b.SetActive(false);
        }
    }

    List<GameObject> bloods = new List<GameObject>();
    void InitBloodEffect(int number)
    {
        for (int x = 0; x < number; x++)
        {
            GameObject blood = Instantiate(bloodPrefab);
            blood.transform.parent = transform.parent;
            blood.SetActive(false);

            bloods.Add(blood);
        }
    }
    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float health)
    {
        this.health = health;
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

}

