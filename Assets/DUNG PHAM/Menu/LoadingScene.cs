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

    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//

    void Start()
    {
        LoadLevel();
    }
    #region LOADING SCENE
    public void LoadLevel()
    {
        StartCoroutine(LoadAsynchronously());
    }
    IEnumerator LoadAsynchronously()
    {
        float x = 0f;
        float t = 0f;

        while (loadingSlider.value < 1)
        {
            if (x > 1) x = 1;
            loadingSlider.value = Mathf.Lerp(0, 1, x);

            percentText.text = $"{(int)(x * 100)} %";

            t = Random.Range(0, 0.5f);
            yield return new WaitForSeconds(t);

            x += Random.Range(0, 0.2f);
        }

        int index = GameManager.instance.index;

        if (loadingSlider.value == 1)
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
    #endregion
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//

}
