using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatabase : MonoBehaviour
{
    [Header("Miscellaneous")]
    public Sprite avatar;
    public Transform head;
    public float maxHealth = 100f;
    public bool isHurt;
    public bool isDied;

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
}