using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public LayerMask playerLayer;
    public Transform[] teleports;
    void Start()
    {
        StartCoroutine(TeleportToNextPoint());

        if (GameManager.instance.SetDifficult() == 0)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
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
            Collider2D player = Physics2D.OverlapCircle(teleports[i].position, 0.3f, playerLayer);

            if (player)
            {
                MovetoPoint(i, player.transform);
                break;
            }
        }
        
        StartCoroutine(TeleportToNextPoint());
    }

    void MovetoPoint(int index, Transform player)
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
