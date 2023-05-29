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
    [SerializeField] AudioSource audioSrc;
    const string LADDER = "Ladder";
    const string WATER = "Water";
    const string SPIKE = "Trap";

    public float climbSpeed, swimSpeed;
    [SerializeField] bool isClimbing, isSwimming, isSpiking;
    float stayTimer;

    [SerializeField] GameObject waterEffectPrefab;
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
        InitWaterEffect(10);
    }

    void Update()
    {
        WaterEffectReset();

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
            // knocker = coli;
            // WaterHurt(knocker);

            WaterImpactEffect(transform.position);

            audioSrc.Play();
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

            WaterImpactEffect(transform.position);
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

    void WaterImpactEffect(Vector2 position)
    {
        Vector2 place = new Vector2(position.x, position.y - 2);

        foreach (GameObject w in waterEffects)
        {
            if (w.activeInHierarchy) continue;
            w.transform.position = place;
            w.SetActive(true);

            break;
        }
    }

    void WaterEffectReset()
    {
        foreach (GameObject w in waterEffects)
        {
            if (w.GetComponent<ParticleSystem>().isPlaying) return;

            w.SetActive(false);
        }
    }

    List<GameObject> waterEffects = new List<GameObject>();
    void InitWaterEffect(int number)
    {
        for (int x = 0; x < number; x++)
        {
            GameObject water = Instantiate(waterEffectPrefab);
            water.transform.parent = transform.parent;
            water.SetActive(false);

            waterEffects.Add(water);
        }
    }
    /**********************************************************************************************************************************/
    /**********************************************************************************************************************************/

}
