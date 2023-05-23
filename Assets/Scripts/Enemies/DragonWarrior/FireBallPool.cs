using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallPool : MonoBehaviour
{
    public static FireBallPool instance;

    List<GameObject> pooledFireBalls = new List<GameObject>();
    int amountToPool = 1;

    [SerializeField]
    GameObject fireBallPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject objPrefab = Instantiate(fireBallPrefab);
            objPrefab.SetActive(false);
            pooledFireBalls.Add(objPrefab);
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
}
