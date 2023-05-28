using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    EnemyAnimation enemyAnimation;
    [SerializeField] Transform[] teleportPoints;
    [SerializeField] float teleportCooldown;
    [SerializeField] float delayTime;
    int index = 0;

    void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
    }


    IEnumerator TeleportCoroutine()
    {
        if (index >= teleportPoints.Length)
            index = 0;

        enemyAnimation.PlayAnimation(6);

        yield return new WaitForSeconds(delayTime);

        transform.position = teleportPoints[index].position;
        index++;

        yield return new WaitForSeconds(teleportCooldown);

        StartCoroutine(TeleportCoroutine());
    }
}
