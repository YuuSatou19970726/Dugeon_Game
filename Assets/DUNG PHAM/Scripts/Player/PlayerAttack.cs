using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform maxRangePoint, minRangePoint;
    public LayerMask attackable;
    int hitCount = 0;
    float comboTimer = 0;
    const string ENEMY = "Enemy";
    public float damage;
    public ScreenShake screenShake;
    public float attackSpeed;
    bool canAttack;
    void Start()
    {
        screenShake = FindObjectOfType<ScreenShake>();
        canAttack = true;
    }

    void Update()
    {
        comboTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

        if (comboTimer > 5f)
        {
            comboTimer = 0;
            hitCount = 0;
        }
    }
    void Attack()
    {
        Collider2D[] targets = Physics2D.OverlapAreaAll(minRangePoint.position, maxRangePoint.position, attackable);

        foreach (Collider2D coli in targets)
        {
            if (coli.CompareTag(ENEMY))
            {
                StartCoroutine(screenShake.cameraShaking());
                
                if (coli.GetComponent<EnemyMelee>())
                    coli.GetComponent<EnemyMelee>().BeAttacked(damage);
                if (coli.GetComponent<EnemyRange>())
                    coli.GetComponent<EnemyRange>().BeAttacked(damage);

                hitCount++;
                comboTimer = 0;

                StartCoroutine(AttackDelay());

                Debug.Log(coli + " + " + hitCount);
            }
        }
    }

    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = Vector3.Lerp(minRangePoint.position, maxRangePoint.position, 0.5f);
        Vector3 size = new Vector3(minRangePoint.position.x - maxRangePoint.position.x, minRangePoint.position.y - maxRangePoint.position.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
}
