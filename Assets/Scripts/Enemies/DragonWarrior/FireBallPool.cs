using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : MonoBehaviour
{
    public static FireBallPool instance;

    List<GameObject> pooledFireBalls = new List<GameObject>();
    int amountToBulletPool = 7;
    float durationBullet = 5f;

    List<GameObject> pooledPinkCoins = new List<GameObject>();
    int amountToCoinPool = 3;
    float durationCoin = 7f;

    [SerializeField]
    GameObject fireBallPrefab;

    [SerializeField]
    GameObject pinkCoinPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToBulletPool; i++)
        {
            GameObject objPrefab = Instantiate(fireBallPrefab);
            objPrefab.SetActive(false);
            pooledFireBalls.Add(objPrefab);
        }

        for (int i = 0; i < amountToCoinPool; i++)
        {
            GameObject objPrefab = Instantiate(pinkCoinPrefab);
            objPrefab.SetActive(false);
            pooledPinkCoins.Add(objPrefab);
        }
    }

    public GameObject GetPooledFireBall()
    {
        int pooledCount = pooledFireBalls.Count;
        if (durationBullet > 0f)
            durationBullet -= Time.deltaTime;

        if (durationBullet > 2.5f && durationBullet < 5.1f)
        {
            pooledCount -= 4;
        } else if (durationBullet > 0f && durationBullet < 2.5f)
        {
            pooledCount -= 2;
        }

        for (int i = 0; i < pooledCount; i++)
        {
            if (!pooledFireBalls[i].activeInHierarchy)
                return pooledFireBalls[i];
        }

        return null;
    }

    public GameObject GetPooledPinkCoin()
    {
        int pooledCount = pooledPinkCoins.Count;

        if (durationCoin > 0f)
            durationCoin -= Time.deltaTime;

        if (durationCoin > 2.5f && durationCoin < 7.1f)
        {
            pooledCount -= 3;
        } else if (durationCoin > 0f && durationCoin < 2.5f)
        {
            pooledCount -= 2;
        }

        if (pooledCount != 0)
        {
            for (int i = 0; i < pooledCount; i++)
            {
                if (!pooledPinkCoins[i].activeInHierarchy)
                    return pooledPinkCoins[i];
            }
        }

        return null;
    }
}
