using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    GameObject enemy;
    bool inQueue;
    public float spawnCooldownTime = 5;

    void Start()
    {
        FirstTimeSpawn();
    }
    void Update()
    {
        if (enemy.activeInHierarchy) return;

        StartCoroutine(ObjectRespawn());
    }

    void FirstTimeSpawn()
    {
        enemy = Instantiate(enemyPrefabs, transform.position, Quaternion.identity);
        enemy.transform.parent = transform;
    }

    IEnumerator ObjectRespawn()
    {
        yield return new WaitForSeconds(spawnCooldownTime);
        enemy.gameObject.SetActive(true);

        // enemy.GetComponentInChildren<EnemyController>().SetHealth();
        // enemy.GetComponentInChildren<EnemyController>().transform.position = transform.position;
        enemy.GetComponentInChildren<EnemyController>().ObjectRespawn(transform.position);
    }
}
