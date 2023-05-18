using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    DataManager dataManager;

    [SerializeField]
    int checkPoint;

    private void Start()
    {
        dataManager = gameObject.AddComponent<DataManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (dataManager.GetCheckPoint() == -1)
            {
                dataManager.SaveCheckPoint(1);
            } else
            {
                if(dataManager.GetCheckPoint() + 1 == checkPoint)
                {
                    dataManager.SaveCheckPoint(checkPoint);
                }
            }
        }
    }
}
