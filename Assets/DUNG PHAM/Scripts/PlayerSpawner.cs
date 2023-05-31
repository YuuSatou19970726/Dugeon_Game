using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    GameObject player;
    Vector2 position;
    float health = 100f;
    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        position = GameManager.instance.SetPosition();
        // health = GameManager.instance.SetHealth();
        player = Instantiate(playerPrefab, position, Quaternion.identity);
        player.GetComponentInChildren<PlayerAttackManager>().SetHealth(health);

        player.transform.parent = transform;
    }
}
