using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    void Start()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            slider.value = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(slider.value);
            yield return new WaitForSeconds(1f);
        }
    }
}
