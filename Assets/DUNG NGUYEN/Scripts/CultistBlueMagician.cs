using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistBlueMagician : MonoBehaviour
{
    bool isFallingWater;
    int countCoinBlue = 0;

    MainGame mainGame;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mainGame.GetIsMovie())
        {
            SetIsFallingWater(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIsFallingWater(bool isFallingWater)
    {
        this.isFallingWater = isFallingWater;
    }

    public bool GetIsFallingWater()
    {
        return isFallingWater;
    }

    public void IncrementCoin()
    {
        countCoinBlue++;
        if (countCoinBlue == 3)
        {
            this.isFallingWater = false;
        }
    }
}
