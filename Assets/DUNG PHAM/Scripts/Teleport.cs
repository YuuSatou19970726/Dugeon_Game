using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public LayerMask playerLayer;
    Transform player;
    public Transform[] teleports;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(TeleportToNextPoint());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator TeleportToNextPoint()
    {
        yield return null;
        for (int i = 0; i < teleports.Length; i++)
        {
            if (Physics2D.OverlapCircle(teleports[i].position, 0.3f, playerLayer))
            {
                MovetoPoint(i);
                break;
            }
        }
        StartCoroutine(TeleportToNextPoint());
    }

    void MovetoPoint(int index)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (index + 1 >= teleports.Length) player.position = teleports[0].position;
            else player.position = teleports[index + 1].position;
        }
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < teleports.Length; i++)
        { Gizmos.DrawWireSphere(teleports[i].position, 0.3f); }
    }
}
