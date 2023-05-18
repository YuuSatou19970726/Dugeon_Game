using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    BaseCurrent baseCurrent;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        baseCurrent = gameObject.AddComponent<BaseCurrent>();
    }

    public void SaveCheckPoint(int position)
    {
        PlayerPrefs.SetInt(baseCurrent.GetCheckPointLevel1(), position);
    }

    public int GetCheckPoint()
    {
        if (PlayerPrefs.HasKey(baseCurrent.GetCheckPointLevel1()))
        {
            return PlayerPrefs.GetInt(baseCurrent.GetCheckPointLevel1());
        }
        return -1;
    }
}
