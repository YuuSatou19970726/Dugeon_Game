using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    DragonWarriorController dragonWarriorController;

    [SerializeField]
    int countGate = 1;

    [SerializeField]
    GameObject[] waypoints;

    float speed = 2f;

    private void Awake()
    {
        dragonWarriorController = FindAnyObjectByType<DragonWarriorController>();
    }

    private void Update()
    {
        switch (countGate)
        {
            case 1:
                if (dragonWarriorController.GetCountGate1() != 2)
                {
                    if (Vector2.Distance(waypoints[dragonWarriorController.GetCountGate1()].transform.position, transform.position) > .1f)
                        transform.position = Vector2.MoveTowards(transform.position, waypoints[dragonWarriorController.GetCountGate1()].transform.position, Time.deltaTime * speed);
                }
                break;
            case 2:
                if (dragonWarriorController.GetCountGate2() != 2)
                {
                    if (Vector2.Distance(waypoints[dragonWarriorController.GetCountGate2()].transform.position, transform.position) > .1f)
                        transform.position = Vector2.MoveTowards(transform.position, waypoints[dragonWarriorController.GetCountGate2()].transform.position, Time.deltaTime * speed);
                }
                break;
        }
    }
}
