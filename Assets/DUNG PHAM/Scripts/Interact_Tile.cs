using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Tile : MonoBehaviour
{
    Rigidbody2D rigid;
    public float climbSpeed, swimSpeed;
    const string LADDER = "Ladder";
    const string WATER = "Water";
    bool isClimbing, isSwimming;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isClimbing)
        {
            rigid.gravityScale = 0f;
            
            rigid.velocity = new Vector3(rigid.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed, 0);
        }
        else
        {
            rigid.gravityScale = 1f;
        }

        if (isSwimming)
        {
            rigid.gravityScale = 0.5f;

            rigid.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * swimSpeed, Input.GetAxisRaw("Vertical") * swimSpeed, 0);
        }
    }
    void OnTriggerStay2D(Collider2D coli)
    {
        if (coli.gameObject.tag == (LADDER)) isClimbing = true;
        else isClimbing = false;
        if (coli.gameObject.tag == (WATER)) isSwimming = true;
        else isSwimming = false;
    }
}
