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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstallBringer());
    }

    private void Update()
    {

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
