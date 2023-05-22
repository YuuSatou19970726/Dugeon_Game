using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    DataManager dataManager;
    MainGame mainGame;

    [SerializeField]
    GameObject food;

    [SerializeField]
    int checkPoint;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    private void Start()
    {
        dataManager = gameObject.AddComponent<DataManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (dataManager.GetCheckPoint() == -1 || dataManager.GetCheckPoint() + 1 == checkPoint)
            {
                mainGame.ChangeCheckPoint();
                StartCoroutine(SpawnFood());
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
