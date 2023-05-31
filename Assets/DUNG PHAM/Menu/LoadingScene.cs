using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LoadingScene : MonoBehaviour
{
    [Header("Loading Scene")]
    public GameObject loadingScene;
    public Slider loadingSlider;
    public TextMeshProUGUI percentText;

    [SerializeField] GameObject[] backgrounds;

    [SerializeField] int index;
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//

    void Start()
    {
        LoadLevel();
    }
    #region LOADING SCENE
    void LoadLevel()
    {
        ChooseBackground();

        StartCoroutine(LoadAsynchronously());
    }
    IEnumerator LoadAsynchronously()
    {
        index = GameManager.instance.index;
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        
        operation.allowSceneActivation = false;

        float x = 0f;
        float t = 0f;

        while (!operation.isDone)
        {
            if (x > 1) x = 1;
            loadingSlider.value = Mathf.Lerp(0, 1, x);

            percentText.text = $"{(int)(x * 100)} %";

            t = Random.Range(0, 0.5f);
            yield return new WaitForSeconds(t);

            x += Random.Range(0, 0.2f);

            if (x >= 1)
            {
                operation.allowSceneActivation = true;
            }
        }
    }

    void ChooseBackground()
    {
        int x = Random.Range(0, backgrounds.Length);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == x) continue;
            backgrounds[i].SetActive(false);
        }
    }
    #endregion
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//

}
