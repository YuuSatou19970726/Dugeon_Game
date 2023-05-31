using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    GameObject spark_bullet_1, spark_bullet_2, spark_bullet_3;

    [SerializeField]
    Text textScore;

    int positionCheckPoint = 0;
    int heart = 0;
    int score = 0;
    int skill = 0;

    //Gates
    int countOpenGate = 5; 
    int countGate_1 = 2;
    int countGate_2 = 1;

    FireBallZone fireBallZone;

    private void Awake()
    {
        fireBallZone = FindAnyObjectByType<FireBallZone>();
    }

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

        if (dataManager.GetCheckPoint() != -1)
            positionCheckPoint = dataManager.GetCheckPoint();

        if (dataManager.GetHeart() != -1)
            heart = dataManager.GetHeart();

        score = dataManager.GetScore();

        CheckSavePoint(positionCheckPoint);
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

    public int GetHeart()
    {
        return heart;
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

    public void DecreaseSkill()
    {
        skill--;
        SparkBulletSkill(skill);
    }

    void SparkBulletSkill(int count)
    {
        switch (count)
        {
            case 1:
                spark_bullet_1.SetActive(true);
                spark_bullet_2.SetActive(false);
                spark_bullet_3.SetActive(false);
                break;
            case 2:
                spark_bullet_1.SetActive(true);
                spark_bullet_2.SetActive(true);
                spark_bullet_3.SetActive(false);
                break;
            case 3:
                spark_bullet_1.SetActive(true);
                spark_bullet_2.SetActive(true);
                spark_bullet_3.SetActive(true);
                break;
            default:
                spark_bullet_1.SetActive(false);
                spark_bullet_2.SetActive(false);
                spark_bullet_3.SetActive(false);
                break;
        }
    }

    void CheckSavePoint (int checkPoint)
    {
        Vector2 position = new Vector2(-23f, 0f);

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
                skill = 3;
                break;
        }
        SparkBulletSkill(skill);
        Instantiate(blueSlime, position, Quaternion.identity);
    }

    public void DecreaseCountOpenGate()
    {
        if (countOpenGate != 0)
            countOpenGate--;

        if (countOpenGate == 0)
            SetCountGate2();
    }

    public int GetCountOpenGate()
    {
        return countOpenGate;
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
        countGate_2 = 0;
        fireBallZone.SetActiveBullet();
    }

    public void ActiveFireBall()
    {
        fireBallZone.SetActiveBullet();
    }

    public void SaveAll()
    {
        dataManager.SaveHeart(heart);
        dataManager.SaveScore(score);
    }
}