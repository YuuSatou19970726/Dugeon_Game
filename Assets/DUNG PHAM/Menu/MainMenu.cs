using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public Dropdown difficultOption;
    public Slider volumnSlider;
    public GameObject loadingScrene;
    public Slider loadingSlider;
    public TextMeshProUGUI percentText;


    void Start()
    {
        ShowMainMenu();
    }


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScrene.SetActive(true);

        float x = 0f;
        float t = 0f;

        // while (!operation.isDone)
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

    public void ShowOptionMenu()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);

        volumnSlider.value = GameManager.instance.volume;
        difficultOption.value = GameManager.instance.difficult;
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);

        loadingScrene.SetActive(false);
        optionMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GetDifficult(int choice)
    {
        GameManager.instance.difficult = choice;
    }

    public void GetVolumn(float value)
    {
        GameManager.instance.volume = value;
    }


}
