using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    GameObject blueSlime;

    [SerializeField]
    GameObject bringer;

    [SerializeField]
    bool isMovie = false;

    float startTime = -1f;
    float roundTime = 10f;
    [SerializeField]
    Text timeValue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstallBringer());
    }

    private void Update()
    {
        if (startTime >= 0f)
        {
            startTime += Time.deltaTime;
            timeValue.text = $"{startTime:0.0}s";
        }
    }

    IEnumerator InstallBringer()
    {
        yield return new WaitForSeconds(.3f);
        Vector2 position = new Vector2(-18.9f, 0f);
        Instantiate(bringer, position, Quaternion.identity);

        StartCoroutine(CreateBLueSlime());

    }

    IEnumerator CreateBLueSlime()
    {
        yield return new WaitForSeconds(.3f);
        Vector2 position = new Vector2(-19f, 0f);
        Instantiate(blueSlime, position, Quaternion.identity);
    }

    public bool GetIsMovie()
    {
        return isMovie;
    }
}
