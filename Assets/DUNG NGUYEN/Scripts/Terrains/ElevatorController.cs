using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField]
    GameObject[] waypoints;

    float deplay = 1.5f;

    float speed = 2f;

    bool checkPlayer = false;

    void Update()
    {
        if (checkPlayer && deplay > 0)
            deplay -= Time.deltaTime;

        if (deplay < 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[1].transform.position, Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
            checkPlayer = true;
        }
    }
}
