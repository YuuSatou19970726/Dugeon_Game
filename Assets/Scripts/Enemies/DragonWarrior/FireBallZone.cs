using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallZone : MonoBehaviour
{
    bool isActiveBullet = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveBullet)
            Fire();
    }

    void Fire()
    {
        GameObject bullet = FireBallPool.instance.GetPooledFireBall();

        Vector2 bodyPosition = transform.position;
        bodyPosition.x += Random.Range(-10f, 10f);

        if (bullet != null)
        {
            bullet.transform.position = bodyPosition;
            Debug.Log("x: " + bullet.transform.position.x);
            Debug.Log("y: " + bullet.transform.position.y);
            bullet.SetActive(true);
        }
    }

    public void SetActiveBullet()
    {
        isActiveBullet = !isActiveBullet;
    }
}
