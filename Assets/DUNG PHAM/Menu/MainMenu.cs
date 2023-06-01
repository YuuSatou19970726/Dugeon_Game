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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) ShowMainMenu();
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
        GetData();
    }

    public void GetVolume(float value)
    {
        volume = value;
        GetData();
    }
    #endregion
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//
    #region LEVEL SELECT
    public void LevelSelect(int index)
    {
        GameManager.instance.LoadLevel(index);
    }
    #endregion
    //********************************************************************************************************************************************//
    //********************************************************************************************************************************************//

}
