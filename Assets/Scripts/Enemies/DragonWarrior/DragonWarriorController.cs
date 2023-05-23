using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWarriorController : MonoBehaviour
{
    BaseCurrent baseCurrent;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;

    float rangeCharacter = 2.5f;
    float colliderDistance = 0.75f;

    [SerializeField]
    LayerMask jumpableGround;

    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    BoxCollider2D boxCollider2DCallGate;

    //Gates
    int countGate_1 = 2;
    int countGate_2 = 2;

    //Animation States
    string currentState = "Slime_Idle_Animation";

    //animator
    Animator animator;
    float animatorDeplay = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        baseCurrent = gameObject.AddComponent<BaseCurrent>();

        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInSight())
        {
            countGate_1 = 1;
            countGate_2 = 1;
        }
    }

    bool PlayerInSight()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2DCallGate.bounds.center + transform.right * rangeCharacter * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider2DCallGate.bounds.size.x * rangeCharacter, boxCollider2DCallGate.bounds.size.y, boxCollider2DCallGate.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        return raycastHit2D.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider2DCallGate.bounds.center + transform.right * rangeCharacter * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider2DCallGate.bounds.size.x * rangeCharacter, boxCollider2DCallGate.bounds.size.y, boxCollider2DCallGate.bounds.size.z));
    }

    public int GetCountGate1()
    {
        return countGate_1;
    }

    public int GetCountGate2()
    {
        return countGate_2;
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
