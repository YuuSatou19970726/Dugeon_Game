using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFood : MonoBehaviour
{
    DataManager dataManager;
    MainGame mainGame;

    int randomFood;

    [SerializeField]
    Sprite[] foodPic;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dataManager = gameObject.AddComponent<DataManager>();
        randomFood = Random.Range(0, foodPic.Length);
        GetComponent<SpriteRenderer>().sprite = foodPic[randomFood];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mainGame.IncrementHeart();
            Destroy(gameObject);
        }
    }
}
