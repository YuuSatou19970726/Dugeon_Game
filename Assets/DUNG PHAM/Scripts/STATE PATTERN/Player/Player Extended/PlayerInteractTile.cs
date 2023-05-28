using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractTile : MonoBehaviour
{
    #region Variables Declared

    PlayerDatabase playerDatabase;
    Rigidbody2D rigid;

    const string LADDER = "Ladder";
    const string WATER = "Water";
    const string ENEMY = "Enemy";

    public float climbSpeed, swimSpeed;
    [SerializeField] bool isClimbing, isSwimming;
    float stayTimer;
    #endregion

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerDatabase = GetComponent<PlayerDatabase>();
    }

    void Update()
    {
        LadderClimb();
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (playerDatabase.isDied) return;

        if (coli.gameObject.tag == (LADDER))
        {
            isClimbing = true;
        }

        WaterHurt(coli);
        SpikeHurt(coli);
    }

    /**********************************************************************************************************************************/

    void OnTriggerStay2D(Collider2D coli)
    {
        if (playerDatabase.isDied) return;

        stayTimer += Time.deltaTime;

        if (stayTimer > 0.5f)
        {
            WaterHurt(coli);
        }
    }
    /**********************************************************************************************************************************/

    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.gameObject.tag == (LADDER))
        {
            isClimbing = false;
        }
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/
    void BeKnockBack(Transform knocker)
    {
        Vector2 knockWay = transform.position - knocker.position;

        int knockX = knockWay.x < 0 ? -1 : 1;
        int knockY = knockWay.y < 0 ? -1 : 1;

        knockWay = new Vector2(knockX, knockY);

        rigid.velocity = knockWay * 15f;
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/
    void SpikeHurt(Collider2D coli)
    {
        if (coli.gameObject.layer != LayerMask.NameToLayer(ENEMY)) return;

        GetComponent<IDamageable>().GetDamage(10);
        BeKnockBack(coli.transform);

        stayTimer = 0f;
    }

    /**********************************************************************************************************************************/

    void WaterHurt(Collider2D coli)
    {
        if (coli.gameObject.tag != (WATER)) return;

        GetComponent<IDamageable>().GetDamage(10);
        stayTimer = 0f;
    }

    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

    void LadderClimb()
    {
        if (isClimbing)
        {
            rigid.gravityScale = 0f;

            rigid.velocity = new Vector3(rigid.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed, 0);
        }
    }

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
