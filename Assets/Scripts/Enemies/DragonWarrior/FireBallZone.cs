using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallZone : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        GameObject bullet = FireBallPool.instance.GetPooledFireBall();

        Vector2 bodyPosition = transform.position;
        bodyPosition.x += Random.Range(0f, boxCollider2D.size.magnitude);

        if (bullet != null)
        {
            bullet.transform.position = bodyPosition;
            bullet.SetActive(true);
        }
    }
}
