using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string sceneName;
    CrossfadeEffect crossfade;

    void Awake()
    {
        crossfade = FindObjectOfType<CrossfadeEffect>();
    }
    void OnEnable()
    {
        StartCoroutine(SceneChangeCoroutine());
    }
    void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator SceneChangeCoroutine()
    {
        crossfade.PlayCrossfadeOut();

        yield return new WaitForSeconds(1);

        ChangeScene();
    }
}
