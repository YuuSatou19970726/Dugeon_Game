using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class PlayerProperties : ScriptableObject
{
    [Header("Player Properties")]
    public float maxHealth;
    public bool isDead;
    public bool isHurt;

    [Header("Player Movement")]
    public float maxSpeed;
    public float jumpForce;
    public float accelerationRate;
    public LayerMask groundLayer, wallLayer;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isLeftWall;
    [HideInInspector] public bool isRightWall;
    [HideInInspector] public bool isWallJumping;
    [HideInInspector] public bool moveOnAir;


    [Header("Player Attack")]
    public float attackSpeed;
    public LayerMask attackLayer;
    public float damage;


    [Header("Ladder")]
    public float climbSpeed;


    [Header("Swimming")]
    public float swimSpeed;


    [Header("Far Reach")]
    public LayerMask farReachLayer;
    public float maxRange;
    public float farReachReleaseTime;
    public float spotBoostForce;


    [Header("Input Controller")]
    public float inputX;
    public float inputXRaw;
    public float inputY;
    public float inputYRaw;
    [HideInInspector] public bool isHorizontalPress, isVerticalPress, isJumpPress, isLeftMousePress, isRightMousePress;
}
