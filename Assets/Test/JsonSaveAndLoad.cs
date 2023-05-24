using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class JsonSaveAndLoad : MonoBehaviour
{
    public InputField input;
    public void SaveToJson()
    {
        Data data = new Data();
        data.sentense = input.text;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/GameOption.json", json);
    }

    public void LoadToJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/GameOption.json");
        Data data = JsonUtility.FromJson<Data>(json);

        input.text = data.sentense;
    }
}
