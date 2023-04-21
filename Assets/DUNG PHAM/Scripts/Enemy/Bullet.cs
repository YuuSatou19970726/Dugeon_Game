using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public EnemyRange enemyRange;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        enemyRange = transform.parent.Find("Enemy").GetComponent<EnemyRange>();  
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            playerHealth.BeDamaged(enemyRange.damage);
        }
        if (coli.gameObject.tag == "Ground") Destroy(gameObject);
    }
}
