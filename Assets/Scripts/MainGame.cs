using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    DataManager dataManager;

    [SerializeField]
    GameObject blueSlime;

    [SerializeField]
    GameObject bringer;

    [SerializeField]
    bool isMovie = false;

    //Display
    [SerializeField]
    GameObject heart_1, heart_2, heart_3;

    [SerializeField]
    Text textScore;

    int heart = 0;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        dataManager = gameObject.AddComponent<DataManager>();
        StartCoroutine(InstallBringer());
    }

    IEnumerator InstallBringer()
    {
        yield return new WaitForSeconds(.3f);

        if (dataManager.GetCheckPoint() == -1)
        {
            Vector2 position = new Vector2(-22.9f, 0f);
            Instantiate(bringer, position, Quaternion.identity);
        }

        StartCoroutine(CreateBLueSlime());

    }

    IEnumerator CreateBLueSlime()
    {
        yield return new WaitForSeconds(.3f);
        Vector2 position = new Vector2(-23f, 0f);
        //dataManager.SaveCheckPoint(3);
        switch (dataManager.GetCheckPoint())
        {
            case 1:
                position = new Vector2(-0.75f, 0f);
                break;
            case 2:
                position = new Vector2(31f, 0f);
                break;
            case 3:
                position = new Vector2(79f, 4f);
                break;
        }

        Instantiate(blueSlime, position, Quaternion.identity);

        if(dataManager.GetHeart() != -1)
        {
            heart = dataManager.GetHeart();
        }
        score = dataManager.GetScore();

        SetScore();
        CheckHeart(heart);
    }

    public bool GetIsMovie()
    {
        return isMovie;
    }

    public void ChangeCheckPoint()
    {
        dataManager.SaveCheckPoint();
        dataManager.SaveScore(this.score);
    }

    public void IncrementScore (int score)
    {
        this.score += score;
        SetScore();
    }

    void SetScore()
    {
        textScore.text = $"{this.score:0000}";
    }

    public void IncrementHeart()
    {
        heart++;
        dataManager.SaveHeart(heart);
        CheckHeart(heart);
    }

    public void DecreaseHeart()
    {
        heart--;
        CheckHeart(heart);
    }

    void CheckHeart(int count)
    {
        switch (count)
        {
            case 1:
                heart_1.SetActive(true);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                break;
            case 2:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(false);
                break;
            case 3:
                heart_1.SetActive(true);
                heart_2.SetActive(true);
                heart_3.SetActive(true);
                break;
            default:
                heart_1.SetActive(false);
                heart_2.SetActive(false);
                heart_3.SetActive(false);
                break;
        }
    }

    public void SaveAll()
    {
        dataManager.SaveHeart(heart);
        dataManager.SaveScore(score);
    }
}