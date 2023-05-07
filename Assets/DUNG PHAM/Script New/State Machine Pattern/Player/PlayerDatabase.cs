using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Database", menuName = "Scriptable Objects")]
public class PlayerDatabase : ScriptableObject
{
    public float moveSpeed = 10f;

    public LayerMask groundLayer;
    public float jumpForce = 10f;
    public float maxCoyoteTime;

    public LayerMask wallLayer;
    public float jumpHeight = 3f;
    public float jumpDistance = 3f;
    public float timeJump = 0.7f;


    public float dashingPower = 20f;
    public float dashingTime = 0.5f;
    public float dashingCooldown = 1f;

    public float maxHealth = 100f;

    public bool isHurt;
    public bool isDied;
}
