using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public TMP_Dropdown difficultOption;
    int difficult;
    public Slider volumnSlider;
    float volume;


    void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        SetData();

        volumnSlider.value = volume;
        difficultOption.value = difficult;
    }
    public void ResetGameButton()
    {
        GameManager.instance.ResetGameJson();
        Pause();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

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
}
