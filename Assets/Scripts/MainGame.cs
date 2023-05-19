using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    DataManager dataManager;

    [SerializeField]
    GameObject blueSlime;

    [SerializeField]
    GameObject bringer;

    [SerializeField]
    bool isMovie = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        dataManager = gameObject.AddComponent<DataManager>();
        StartCoroutine(InstallBringer());
    }

    IEnumerator InstallBringer()
    {
        yield return new WaitForSeconds(.3f);

        if (dataManager.GetCheckPoint() == -1)
        {
            Vector2 position = new Vector2(-22.9f, 0f);
            Instantiate(bringer, position, Quaternion.identity);
        }

        StartCoroutine(CreateBLueSlime());

    }

    IEnumerator CreateBLueSlime()
    {
        yield return new WaitForSeconds(.3f);
        Vector2 position = new Vector2(-23f, 0f);
        //dataManager.SaveCheckPoint(3);
        switch (dataManager.GetCheckPoint())
        {
            case 1:
                position = new Vector2(-0.75f, 0f);
                break;
            case 2:
                position = new Vector2(31f, 0f);
                break;
            case 3:
                position = new Vector2(79f, 4f);
                break;
        }

        Instantiate(blueSlime, position, Quaternion.identity);
    }

    public bool GetIsMovie()
    {
        return isMovie;
    }
}