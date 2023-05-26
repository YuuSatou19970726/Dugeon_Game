using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallZone : MonoBehaviour
{
    MainGame mainGame;

    bool isActiveBullet = false;
    float deplayTime = .75f;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveBullet)
        {
            Fire();

            if (deplayTime > 0)
            {
                deplayTime -= Time.deltaTime;
            }
            else
            {
                Coin();
            }
        }
    }

    void Fire()
    {
        GameObject bullet = FireBallPool.instance.GetPooledFireBall();

        Vector2 bodyPosition = transform.position;
        bodyPosition.x += Random.Range(-10f, 10f);

        if (bullet != null)
        {
            bullet.transform.position = bodyPosition;
            bullet.SetActive(true);
        }
    }

    void Coin()
    {
        GameObject coin = FireBallPool.instance.GetPooledPinkCoin();

        Vector2 bodyPosition = transform.position;
        bodyPosition.x += Random.Range(-10f, 10f);

        if (coin != null)
        {
            coin.transform.position = bodyPosition;
            coin.SetActive(true);
        }
    }

    public void SetActiveBullet()
    {
        isActiveBullet = !isActiveBullet;
    }
}
