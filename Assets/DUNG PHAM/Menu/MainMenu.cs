using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject mainMenu;

    [Header("Option Menu")]
    public GameObject optionMenu;
    public TMP_Dropdown difficultOption;
    int difficult;
    public Slider volumnSlider;
    float volume;

    [Header("Level Select")]
    public GameObject levelSelectScene;

    [Header("Loading Scene")]
    public GameObject loadingScrene;
    public Slider loadingSlider;
    public TextMeshProUGUI percentText;

    void Start()
    {
        ShowMainMenu();
    }


    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//
    #region BUTTON FUNCTION
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);

        loadingScrene.SetActive(false);
        optionMenu.SetActive(false);
        levelSelectScene.SetActive(false);
    }

    public void ShowOptionMenu()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);

        SetData();

        volumnSlider.value = volume;
        difficultOption.value = difficult;
    }

    public void ShowLevelSelectMenu()
    {
        levelSelectScene.SetActive(true);

        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackToMainMenuButton()
    {
        ShowMainMenu();
        GetData();
    }

    public void ResetGameButton()
    {
        GameManager.instance.ResetGameJson();
        ShowOptionMenu();
    }
    #endregion
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//
    #region OPTION DATA
    void SetData()
    {
        volume = GameManager.instance.SetVolume();
        difficult = GameManager.instance.SetDifficult();
    }

    void GetData()
    {
        GameManager.instance.GetGameData(difficult, volume);
    }

    public void GetDifficult(int choice)
    {
        difficult = choice;
    }

    public void GetVolume(float value)
    {
        volume = value;
    }
    #endregion
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//
    #region LOADING SCENE
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScrene.SetActive(true);
        levelSelectScene.SetActive(false);

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

        if (loadingSlider.value == 1)
            SceneManager.LoadSceneAsync(sceneIndex);
    }
    #endregion
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//

}
