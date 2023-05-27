using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    GameObject player;
    Vector2 position;
    void Start()
    {
        position = GameManager.instance.SetPosition();

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, position, Quaternion.identity);
        player.transform.parent = transform;
    }
}
