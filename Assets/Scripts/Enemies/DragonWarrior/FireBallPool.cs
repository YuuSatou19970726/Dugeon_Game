using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : MonoBehaviour
{
    public static FireBallPool instance;

    List<GameObject> pooledFireBalls = new List<GameObject>();
    int amountToBulletPool = 5;

    List<GameObject> pooledPinkCoins = new List<GameObject>();
    int amountToCoinPool = 3;

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
        for (int i = 0; i < pooledFireBalls.Count; i++)
        {
            if (!pooledFireBalls[i].activeInHierarchy)
                return pooledFireBalls[i];
        }

        return null;
    }

    public GameObject GetPooledPinkCoin()
    {
        for (int i = 0; i < pooledPinkCoins.Count; i++)
        {
            if (!pooledPinkCoins[i].activeInHierarchy)
                return pooledPinkCoins[i];
        }

        return null;
    }
}
