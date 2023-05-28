using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    string PLAYER = "Player";
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.CompareTag(PLAYER))
            GameManager.instance.GetPlayerData(transform.position, coli.GetComponent<IDamageable>().GetHealth());
    }
}
