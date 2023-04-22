using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Fields
    [SerializeField] PlayerController playerController;
    public Transform maxRangePoint, minRangePoint;
    public LayerMask attackable;
    int hitCount = 0;
    float comboTimer = 0;
    const string ENEMY = "Enemy";
    public float damage;
    public ScreenShake screenShake;
    public float attackSpeed;
    bool attack01, attack02, attack03;
    #endregion

    #region MonoBehaviour
    void Start()
    {
        screenShake = FindObjectOfType<ScreenShake>();
    }

    void Update()
    {
        comboTimer += Time.deltaTime;

        if (comboTimer > 5f)
        {
            comboTimer = 0;
            hitCount = 0;
        }

        Attack();
    }
    #endregion

    #region Attack
    void AttackAndPlayAnimation(string clip)
    {
        playerController.PlayAnimation(clip);

        Collider2D[] targets = Physics2D.OverlapAreaAll(minRangePoint.position, maxRangePoint.position, attackable);

        foreach (Collider2D coli in targets)
        {
            if (coli.CompareTag(ENEMY))
            {

                if (coli.GetComponent<EnemyMelee>())
                {
                    coli.GetComponent<EnemyMelee>().BeAttacked(damage);
                }
                if (coli.GetComponent<EnemyRange>())
                {
                    coli.GetComponent<EnemyRange>().BeAttacked(damage);
                }

                StartCoroutine(screenShake.cameraShaking());

                hitCount++;
                comboTimer = 0;

                Debug.Log(coli + " + " + hitCount);
            }
        }
    }
    void Attack()
    {
        if (
        Input.GetMouseButtonDown(0)
        && !playerController.state.IsName(playerController.ATTACK[0])
        && !playerController.state.IsName(playerController.ATTACK[1])
        && !playerController.state.IsName(playerController.ATTACK[2]))
        {
            AttackAndPlayAnimation(playerController.ATTACK[0]);
            return;
        }

        if (playerController.state.IsName(playerController.ATTACK[0]))
        {
            if (Input.GetMouseButtonDown(0)) attack02 = true;

            if (playerController.state.normalizedTime >= 1 && attack02)
            {
                AttackAndPlayAnimation(playerController.ATTACK[1]);
                attack02 = false;
                return;
            }
            else if (playerController.state.normalizedTime >= 1.5 && !attack02)
            {
                playerController.MoveAnim(playerController.IDLE);
            }
        }

        if (playerController.state.IsName(playerController.ATTACK[1]))
        {
            if (Input.GetMouseButtonDown(0))
            {
                attack03 = true;
            }

            if (playerController.state.normalizedTime >= 1 && attack03)
            {
                AttackAndPlayAnimation(playerController.ATTACK[2]);

                attack03 = false;
                return;
            }
            else if (playerController.state.normalizedTime >= 1.5 && !attack03)
            {
                playerController.MoveAnim(playerController.IDLE);
            }
        }

        if (playerController.state.IsName(playerController.ATTACK[2]))
        {
            if (Input.GetMouseButtonDown(0))
            {
                attack01 = true;
            }

            if (playerController.state.normalizedTime >= 1 && attack01)
            {
                AttackAndPlayAnimation(playerController.ATTACK[0]);
                attack01 = false;
                return;
            }
            else if (playerController.state.normalizedTime >= 1.5 && !attack01)
            {
                playerController.MoveAnim(playerController.IDLE);
            }
        }
    }
    #endregion

    #region Debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = Vector3.Lerp(minRangePoint.position, maxRangePoint.position, 0.5f);
        Vector3 size = new Vector3(minRangePoint.position.x - maxRangePoint.position.x, minRangePoint.position.y - maxRangePoint.position.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
    #endregion
}
