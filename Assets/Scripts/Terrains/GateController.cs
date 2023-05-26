using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    MainGame mainGame;

    [SerializeField]
    int countGate = 1;

    [SerializeField]
    GameObject[] waypoints;

    float speed = 2f;

    private void Awake()
    {
        mainGame = FindAnyObjectByType<MainGame>();
    }

    private void Update()
    {
        switch (countGate)
        {
            case 1:
                if (mainGame.GetCountGate1() != 2)
                {
                    if (Vector2.Distance(waypoints[mainGame.GetCountGate1()].transform.position, transform.position) > .1f)
                        transform.position = Vector2.MoveTowards(transform.position, waypoints[mainGame.GetCountGate1()].transform.position, Time.deltaTime * speed);
                }
                break;
            case 2:
                if (mainGame.GetCountGate2() != 2)
                {
                    if (Vector2.Distance(waypoints[mainGame.GetCountGate2()].transform.position, transform.position) > .1f)
                        transform.position = Vector2.MoveTowards(transform.position, waypoints[mainGame.GetCountGate2()].transform.position, Time.deltaTime * speed);
                }
                break;
        }
    }
}
