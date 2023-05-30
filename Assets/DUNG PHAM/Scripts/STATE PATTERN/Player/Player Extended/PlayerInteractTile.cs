using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractTile : MonoBehaviour
{
    #region Variables Declared

    PlayerDatabase playerDatabase;
    PlayerAttackManager playerAttack;
    Rigidbody2D rigid;
    Collider2D knocker;
    const string LADDER = "Ladder";
    const string WATER = "Water";
    const string SPIKE = "Trap";

    public float climbSpeed, swimSpeed;
    [SerializeField] bool isClimbing, isSwimming, isSpiking;
    float stayTimer;

    #endregion

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerDatabase = GetComponent<PlayerDatabase>();
        playerAttack = GetComponent<PlayerAttackManager>();
    }
    void Start()
    {
    }

    void Update()
    {
        if (playerDatabase.isDied) return;

        LadderClimb();

        if (isSpiking)
            if (playerAttack.hurtTimer > 2f)
            {
                SpikeHurt(knocker);
            }
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag(LADDER))
        {
            isClimbing = true;
        }

        if (coli.CompareTag(WATER))
        {
            isSwimming = true;
        }

        if (coli.CompareTag(SPIKE))
        {
            isSpiking = true;
            knocker = coli;
            SpikeHurt(knocker);
        }
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.CompareTag(LADDER))
        {
            isClimbing = false;
        }

        if (coli.CompareTag(WATER))
        {
            isSwimming = false;
        }

        if (coli.CompareTag(SPIKE))
        {
            isSpiking = false;
        }
    }

    /**********************************************************************************************************************************/

    void LadderClimb()
    {
        if (!isClimbing) return;

        rigid.gravityScale = 0f;

        rigid.velocity = new Vector3(rigid.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed, 0);

    }
    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/


    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/
    void SpikeHurt(Collider2D coli)
    {
        if (!isSpiking) return;

        playerAttack.GetDamage(10, coli.transform);

        stayTimer = 0f;
    }

    /**********************************************************************************************************************************/

    void WaterHurt(Collider2D coli)
    {
        if (!isSwimming) return;

        playerAttack.GetDamage(10, coli.transform);
        stayTimer = 0f;
    }

    /**********************************************************************************************************************************/

    void Swimming()
    {
        if (isSwimming)
        {
            rigid.gravityScale = 4.9f;

            if (Input.GetAxisRaw("Vertical") != 0)
                rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * swimSpeed, Input.GetAxisRaw("Vertical") * swimSpeed, 0);
            else
            {
                rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * swimSpeed, rigid.gravityScale, 0);
            }
        }
    }

   
    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

}
