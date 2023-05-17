using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void IsTheFirstAppInstall()
    //{
    //    if (!PlayerPrefs.HasKey("IsTheFirstAppInstall"))
    //    {
    //        PlayerPrefs.SetInt(SCORE_KEY, 0);
    //        PlayerPrefs.SetInt("IsTheFirstAppInstall", 0);
    //    }

    //}

    //public void SaveScore(int score)
    //{
    //    PlayerPrefs.SetInt(SCORE_KEY, score);
    //}

    //public int GetScore()
    //{
    //    return PlayerPrefs.GetInt(SCORE_KEY);
    //}
}
