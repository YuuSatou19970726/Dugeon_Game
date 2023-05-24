using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] string sceneName;
    public Slider volumnSlider;
    public Dropdown difficultOption;

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowOptionMenu()
    {
        volumnSlider.value = GameManager.instance.volume;
        difficultOption.value = GameManager.instance.difficult;
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
