using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Fields
    PlayerController playerController;
    public Transform maxRangePoint, minRangePoint;
    ScreenShake screenShake;
    int hitCount;
    float comboTimer;
    bool attack01, attack02, attack03;

    [SerializeField] ParticleSystem enemyBlood;

    #endregion

    #region MonoBehaviour
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        screenShake = FindObjectOfType<ScreenShake>();
    }
    void Start()
    {
        enemyBlood.Stop();
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
        playerController.playerAnimation.PlayAnimation(clip);

        Collider2D[] targets = Physics2D.OverlapAreaAll(minRangePoint.position, maxRangePoint.position, playerController.playerProperties.attackLayer);

        foreach (Collider2D coli in targets)
        {
            // if (coli.GetComponent<EnemyBeAttacked>())
            // {
            //     coli.GetComponent<EnemyBeAttacked>().BeHurt(playerController.playerProperties.damage);
            // }

            if (coli.GetComponent<IDamageable>() != null)
            {
                coli.GetComponent<IDamageable>().GetDamage(playerController.playerProperties.damage);

                enemyBlood.Play();
            }

            StartCoroutine(screenShake.cameraShaking());


            hitCount++;
            comboTimer = 0;

            Debug.Log(coli + " + " + hitCount);

        }
    }
    void Attack()
    {
        if (playerController.playerAnimation.CheckCurrentAnimation(playerController.playerAnimation.ATTACK[0])
        && playerController.playerAnimation.currentState.normalizedTime < 0.5f) return;

        if (
        playerController.playerProperties.isLeftMousePress
        && !playerController.playerAnimation.CheckCurrentAnimation(playerController.playerAnimation.ATTACK[0])
        && !playerController.playerAnimation.CheckCurrentAnimation(playerController.playerAnimation.ATTACK[1])
        && !playerController.playerAnimation.CheckCurrentAnimation(playerController.playerAnimation.ATTACK[2]))
        {
            AttackAndPlayAnimation(playerController.playerAnimation.ATTACK[0]);
            return;
        }

        if (playerController.playerAnimation.CheckCurrentAnimation(playerController.playerAnimation.ATTACK[0]))
        {
            if (playerController.playerProperties.isLeftMousePress) attack02 = true;

            if (playerController.playerAnimation.currentState.normalizedTime >= 1 && attack02)
            {
                AttackAndPlayAnimation(playerController.playerAnimation.ATTACK[1]);
                attack02 = false;
                return;
            }
            else if (playerController.playerAnimation.currentState.normalizedTime >= 1.5 && !attack02)
            {
                playerController.playerAnimation.MoveAnim(playerController.playerAnimation.IDLE);
            }
        }

        if (playerController.playerAnimation.currentState.IsName(playerController.playerAnimation.ATTACK[1]))
        {
            if (playerController.playerProperties.isLeftMousePress)
            {
                attack03 = true;
            }

            if (playerController.playerAnimation.currentState.normalizedTime >= 1 && attack03)
            {
                AttackAndPlayAnimation(playerController.playerAnimation.ATTACK[2]);

                attack03 = false;
                return;
            }
            else if (playerController.playerAnimation.currentState.normalizedTime >= 1.5 && !attack03)
            {
                playerController.playerAnimation.MoveAnim(playerController.playerAnimation.IDLE);
            }
        }

        if (playerController.playerAnimation.currentState.IsName(playerController.playerAnimation.ATTACK[2]))
        {
            if (playerController.playerProperties.isLeftMousePress)
            {
                attack01 = true;
            }

            if (playerController.playerAnimation.currentState.normalizedTime >= 1 && attack01)
            {
                AttackAndPlayAnimation(playerController.playerAnimation.ATTACK[0]);
                attack01 = false;
                return;
            }
            else if (playerController.playerAnimation.currentState.normalizedTime >= 1.5 && !attack01)
            {
                playerController.playerAnimation.MoveAnim(playerController.playerAnimation.IDLE);
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
