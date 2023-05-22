using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataManager : MonoBehaviour
{
    BaseCurrent baseCurrent;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //}

    // Start is called before the first frame update
    void Start()
    {
        baseCurrent = gameObject.AddComponent<BaseCurrent>();
    }

    public void SaveCheckPoint()
    {
        if (!PlayerPrefs.HasKey(baseCurrent.GetCheckPointLevel1()))
        {
            PlayerPrefs.SetInt(baseCurrent.GetCheckPointLevel1(), 1);
        } else
        {
            PlayerPrefs.SetInt(baseCurrent.GetCheckPointLevel1(), GetCheckPoint() + 1);
        }
    }

    public int GetCheckPoint()
    {
        if (PlayerPrefs.HasKey(baseCurrent.GetCheckPointLevel1()))
        {
            return PlayerPrefs.GetInt(baseCurrent.GetCheckPointLevel1());
        }
        return -1;
    }

    public void SaveHeart(int count)
    {
        PlayerPrefs.SetInt(baseCurrent.GetHeartLevel1(), count);
    }

    public int GetHeart()
    {
        if (PlayerPrefs.HasKey(baseCurrent.GetHeartLevel1()))
        {
            return PlayerPrefs.GetInt(baseCurrent.GetHeartLevel1());
        }
        return -1;
    }
}
