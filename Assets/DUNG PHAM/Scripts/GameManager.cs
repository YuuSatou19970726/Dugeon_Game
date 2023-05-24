using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int difficult = 2;
    public float volume = 1;
    public Vector2 savePoint = Vector2.zero;
    public float playerHealth = 100;

    public static GameManager instance;

    /**********************************************************************************/
    /**********************************************************************************/

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        LoadToJson();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void SaveToJson()
    {
        /**********************************************************************************/
        GameData gameData = new GameData();

        gameData.difficult = difficult;
        gameData.volume = volume;

        string json1 = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(Application.dataPath + "/GameOption.json", json1);

        /**********************************************************************************/

        // PlayerData playerData = new PlayerData();

        // playerData.savePoint = savePoint;
        // playerData.playerHealth = playerHealth;

        // string json2 = JsonUtility.ToJson(playerData, true);
        // File.WriteAllText(Application.dataPath + "/PlayerSave.json", json2);
        /**********************************************************************************/
    }

    public void LoadToJson()
    {
        /**********************************************************************************/
        string json1 = File.ReadAllText(Application.dataPath + "/GameOption.json");
        GameData gameData = JsonUtility.FromJson<GameData>(json1);

        difficult = gameData.difficult;
        volume = gameData.volume;

        /**********************************************************************************/
        // string json2 = File.ReadAllText(Application.dataPath + "/PlayerSave.json");
        // PlayerData playerData = JsonUtility.FromJson<PlayerData>(json2);

        // savePoint = playerData.savePoint;
        // playerHealth = playerData.playerHealth;
        /**********************************************************************************/
    }
}

/**********************************************************************************/
/**********************************************************************************/

[System.Serializable]
public class GameData
{
    public int difficult;
    public float volume;
}

/**********************************************************************************/
/**********************************************************************************/

[System.Serializable]
public class PlayerData
{
    public Vector2 savePoint;
    public float playerHealth;
}

/**********************************************************************************/
/**********************************************************************************/
