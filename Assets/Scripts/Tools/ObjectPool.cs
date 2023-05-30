using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    GameObject fireBallPrefab;
    List<GameObject> pooledFireBalls = new List<GameObject>();
    int amountToBulletPool = 7;
    float durationBullet = 5f;

    [SerializeField]
    GameObject pinkCoinPrefab;
    List<GameObject> pooledPinkCoins = new List<GameObject>();
    int amountToCoinPool = 3;
    float durationCoin = 7f;

    [SerializeField]
    GameObject iblastBulletPrefab;
    List<GameObject> pooledIblastBullet = new List<GameObject>();
    int amountToIblastBulletPool = 5;

    [SerializeField]
    GameObject sparkBulletPrefab;
    List<GameObject> pooledSparkBullet = new List<GameObject>();
    int amountToSparkBulletPool = 3;

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

        for (int i = 0; i < amountToIblastBulletPool; i++)
        {
            GameObject objPrefab = Instantiate(iblastBulletPrefab);
            objPrefab.SetActive(false);
            pooledIblastBullet.Add(objPrefab);
        }

        for (int i = 0; i < amountToSparkBulletPool; i++)
        {
            GameObject objPrefab = Instantiate(sparkBulletPrefab);
            objPrefab.SetActive(false);
            pooledSparkBullet.Add(objPrefab);
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
        }
        else if (durationBullet > 0f && durationBullet < 2.5f)
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
        }
        else if (durationCoin > 0f && durationCoin < 2.5f)
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

    public GameObject GetPooledIblastBullet()
    {

        for (int i = 0; i < pooledIblastBullet.Count; i++)
        {
            if (!pooledIblastBullet[i].activeInHierarchy)
                return pooledIblastBullet[i];
        }

        return null;
    }

    public GameObject GetPooledSparkBullet()
    {

        for (int i = 0; i < pooledSparkBullet.Count; i++)
        {
            if (!pooledSparkBullet[i].activeInHierarchy)
                return pooledSparkBullet[i];
        }

        return null;
    }
}
