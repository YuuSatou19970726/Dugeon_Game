using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    GameObject blueSlime;

    [SerializeField]
    bool isMovie = false;

    // Start is called before the first frame update
    void Start()
    {
        InstallBlueSlime();
    }

    void InstallBlueSlime()
    {
        Vector2 position = new Vector2(-21f, 0f);
        Instantiate(blueSlime, position, Quaternion.identity);
    }

    public bool GetIsMovie()
    {
        return isMovie;
    }
}
