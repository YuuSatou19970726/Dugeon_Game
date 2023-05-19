using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string sceneName;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ChangeScene();
    }
    void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
