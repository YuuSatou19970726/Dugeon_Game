using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public float health;
    public float maxHealth;
    void Start()
    {
        health = maxHealth;
    }
    public void Recovery(float number)
    {
        if (health + number > maxHealth) health = maxHealth;
        else health += number;
    }
    public void BeDamaged(float damage)
    {
        GetComponent<PlayerController>().BeDamagedAnim();
        health -= damage;

        if (health <= 0) Death();
    }

    void Death()
    {
        if (health <= 0)
        {
            StartCoroutine(GetComponent<PlayerController>().BeDeath());
        }
        gameObject.layer = 10;
        GetComponent<PlayerController>().enabled = false;
    }
}
