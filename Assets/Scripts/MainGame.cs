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

    int positionCheckPoint = 0;
    int heart = 0;
    int score = 0;

    //Gates
    int countGate_1 = 2;
    int countGate_2 = 2;

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

        if (dataManager.GetCheckPoint() == -1 && !isMovie)
        {
            Vector2 position = new Vector2(-22.9f, 0f);
            Instantiate(bringer, position, Quaternion.identity);
        }

        if (!isMovie)
            StartCoroutine(CreateBLueSlime());

    }

    IEnumerator CreateBLueSlime()
    {
        yield return new WaitForSeconds(.3f);
        Vector2 position = new Vector2(-23f, 0f);

        if (dataManager.GetCheckPoint() != -1)
            positionCheckPoint = dataManager.GetCheckPoint();

        switch (positionCheckPoint)
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

        if (dataManager.GetHeart() != -1)
            heart = dataManager.GetHeart();

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
        positionCheckPoint++;
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

    public int GetCountGate1()
    {
        return countGate_1;
    }

    public int GetCountGate2()
    {
        return countGate_2;
    }

    public void SetCountGates()
    {
        countGate_1 = 1;
        countGate_2 = 1;
    }

    public void SetCountGate2()
    {
        countGate_2 = 2;
    }

    public void SaveAll()
    {
        dataManager.SaveHeart(heart);
        dataManager.SaveScore(score);
    }
}