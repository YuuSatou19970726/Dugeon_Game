using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    DataManager dataManager;

    [SerializeField]
    GameObject food;

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
                dataManager.SaveCheckPoint();
                StartCoroutine(SpawnFood());
            } else
            {
                if(dataManager.GetCheckPoint() + 1 == checkPoint)
                {
                    dataManager.SaveCheckPoint();
                    StartCoroutine(SpawnFood());
                }
            }
        }
    }

    IEnumerator SpawnFood()
    {
        yield return new WaitForSeconds(.3f);

        Vector2 position = transform.position;
        position.y += 2f;
        Instantiate(food, position, Quaternion.identity);
    }
}
