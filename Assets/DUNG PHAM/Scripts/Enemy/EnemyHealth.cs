using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyProperties enemy;
    public float healthRate;

    void Update()
    {
        DisplayHealth();
    }
    void DisplayHealth()
    {
        healthRate = enemy.health / enemy.maxHealth;

        transform.localScale = new Vector3(healthRate, transform.localScale.y, 0f);
    }
}
