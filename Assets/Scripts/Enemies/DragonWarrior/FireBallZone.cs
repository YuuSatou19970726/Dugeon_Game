using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallZone : MonoBehaviour
{
    [SerializeField]
    GameObject waypoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        GameObject bullet = FireBallPool.instance.GetPooledFireBall();

        if (bullet != null)
        {
            bullet.transform.position = waypoint.transform.position;
            bullet.SetActive(true);
        }
    }
}
