using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Tile : MonoBehaviour
{
    #region Variables Declared
    Rigidbody2D rigid;
    const string LADDER = "Ladder";
    const string WATER = "Water";
    public float climbSpeed, swimSpeed;
    [SerializeField] bool isClimbing, isSwimming;
    #endregion

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isClimbing)
        {
            rigid.gravityScale = 0f;

            rigid.velocity = new Vector3(rigid.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed, 0);
        }

        if (isSwimming)
        {
            rigid.gravityScale = 0.8f;

            rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * swimSpeed, Input.GetAxisRaw("Vertical") * swimSpeed, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.tag == (LADDER))
        {
            isClimbing = true;
        }

        if (coli.gameObject.tag == (WATER)) isSwimming = true;
    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.gameObject.tag == (LADDER))
        {
            isClimbing = false;
        }


        if (coli.gameObject.tag == (WATER)) isSwimming = false;
    }
}
