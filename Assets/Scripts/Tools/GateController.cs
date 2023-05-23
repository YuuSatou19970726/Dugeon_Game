using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    GameObject[] waypoints;

    float speed = 2f;

    bool checkPlayer = false;

    void CloseGate()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[1].transform.position, Time.deltaTime * speed);
    }

    void OpenGate()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[0].transform.position, Time.deltaTime * speed);
    }
}
