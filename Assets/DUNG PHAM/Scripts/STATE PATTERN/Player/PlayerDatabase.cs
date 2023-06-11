using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatabase : MonoBehaviour
{
    [Header("Miscellaneous")]
    public Sprite avatar;
    public Transform head;
    public HealthBar healthBar;
    public float maxHealth = 100f;
    public bool isHurt;
    public bool isDied;

    public AfterImagePool afterImage;

    [Header("Movement")]
    public float moveSpeed = 20f;

    [Header("Ground Jump")]
    public LayerMask groundLayer;
    public float jumpForce = 30f;
    public float maxCoyoteTime = 0.1f;
    public float maxFallVelocity = 20f;
    public int jumpNumber = 1;
    public float gravity = 9.81f;

    [Header("Wall Jump")]
    public LayerMask wallLayer;

    [Header("Dash")]
    public float dashingPower = 30f;
    public float dashingCooldown = 1f;
    public float dashingTime = 0.5f;

    [Header("Attack")]
    public float attackDamage = 10f;
    public float hitResetTime = 3f;
    public float spikeDamage = 5f;

    [Header("Animation")]
    public string IDLE = "Idle";
    public string JUMP = "Jump";
    public string CROUCH = "Crouch";
    public string CROUCH_MOVE = "Crouch Move";
    public string WALLSLIDE = "Wall Slide";
    public string WALLJUMP = "Wall Jump";
    public string LEDGE_GRAB = "Wall Grab";
    public string WALLCLB = "Wall Climb";
    public string DASH = "Wall Jump";
    public string AIR_ATK = "Attack Jump 1";
    public string AIR_ATK_1 = "Attack Jump 2";
    public string AIR_ATK_2 = "Attack Jump 3 Begin";
    public string AIR_ATK_2_LOOP = "Attack Jump 3 Loop";
    public string AIR_ATK_2_END = "Attack Jump 3 End";
    public string SPELL = "Spell Cast";

    void Awake()
    {
        ApplyDifficult();
        SpawnPlayer();
    }
    void ApplyDifficult()
    {
        int ratio = GameManager.instance.SetDifficult();

        spikeDamage *= ratio;
    }
    Vector2 position;
    float health;
    void SpawnPlayer()
    {
        position = GameManager.instance.SetPosition();
        transform.position = position;

        health = GameManager.instance.SetHealth();
        GetComponent<PlayerAttackManager>().SetHealth(health);
    }
}