using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Default Setting")]
    [SerializeField] int defaultDifficult = 2;
    [SerializeField] float defaultVolume = 1;
    [SerializeField] Vector2 defaultSavepoint = Vector2.zero;
    [SerializeField] float defaultHealth = 100f;

    [Header("Variables to get data")]
    int difficult;
    float volume;
    Vector2 savePoint;
    float playerHealth;

    /**********************************************************************************/
    /**********************************************************************************/

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;

    }
    void Start()
    {
        LoadToJson();

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    /***************************************************************************************************************************************************/
    /***************************************************************************************************************************************************/

    public void GetGameData(int difficult, float volume)
    {
        this.difficult = difficult;
        this.volume = volume;

        SaveToJson();
    }

    public void GetPlayerData(Vector2 savePoint, float playerHealth)
    {
        this.savePoint = savePoint;
        this.playerHealth = playerHealth;

        SaveToJson();
    }

    public void ResetGameJson()
    {
        difficult = defaultDifficult;
        volume = defaultVolume;
        savePoint = defaultSavepoint;
        playerHealth = defaultHealth;

        PlayerPrefs.DeleteAll();

        SaveToJson();
    }
    /***************************************************************************************************************************************************/
    /***************************************************************************************************************************************************/

    public void SaveToJson()
    {
        GameData gameData = new GameData();

        gameData.difficult = difficult;
        gameData.volume = volume;
        gameData.savePoint = savePoint;
        gameData.playerHealth = playerHealth;

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(Application.dataPath + "/SaveData.json", json);
    }

    public void LoadToJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/SaveData.json");
        GameData gameData = JsonUtility.FromJson<GameData>(json);

        difficult = gameData.difficult;
        volume = gameData.volume;
        savePoint = gameData.savePoint;
        playerHealth = gameData.playerHealth;
    }

    /***************************************************************************************************************************************************/
    /***************************************************************************************************************************************************/

    public float SetVolume()
    {
        return volume;
    }
    public int SetDifficult()
    {
        return difficult;
    }
    public Vector2 SetPosition()
    {
        return savePoint;
    }
    public float SetHealth()
    {
        return playerHealth;
    }

    /***************************************************************************************************************************************************/
    public int index;
    public void LoadLevel(int index)
    {
        SceneManager.LoadSceneAsync(1);

        this.index = index;
    }



}

/***************************************************************************************************************************************************/
/***************************************************************************************************************************************************/

[System.Serializable]
public class GameData
{
    public int difficult;
    public float volume;
    public Vector2 savePoint;
    public float playerHealth;
}