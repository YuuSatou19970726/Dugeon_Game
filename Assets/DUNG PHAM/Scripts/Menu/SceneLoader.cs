using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) SceneManager.LoadSceneAsync(sceneName);
    }
    void OnTriggerEnter2D(Collider2D coli)
    {
        if (!coli.CompareTag("Player")) return;

        SceneManager.LoadSceneAsync(sceneName);
    }
}
