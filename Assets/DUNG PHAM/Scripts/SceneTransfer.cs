using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    [SerializeField] string sceneName;
    bool isLoaded = false;
    string PLAYER_TAG = "Player";

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (isLoaded) return;

        if (!coli.CompareTag(PLAYER_TAG)) return;

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        isLoaded = true;

    }

    void OnTriggerExit2D(Collider2D coli)
    {
        if (!isLoaded) return;

        if (!coli.CompareTag(PLAYER_TAG)) return;

        SceneManager.UnloadSceneAsync(sceneName);

        isLoaded = false;
    }
}
