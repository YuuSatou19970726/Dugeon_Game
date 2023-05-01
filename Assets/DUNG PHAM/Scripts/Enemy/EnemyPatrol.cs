using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    EnemyController enemyController;
    Transform target;
    public Transform[] patrolPoints = new Transform[2];
    int index = 0;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    void Start()
    {
        target = patrolPoints[index];
    }

    void Update()
    {
        if (enemyController.enemyProperties.playerDetected) return;
        
        Patrol(target);
    }
    public void Patrol(Transform target)
    {
        if (target != patrolPoints[index]) return;

        enemyController.MoveToTarget(target.position);

        if (Vector2.Distance(transform.position, target.position) > 0.5f) return;

        index++;

        if (index >= patrolPoints.Length)
        {
            index = 0;
        }

        StartCoroutine(PatrolRest(patrolPoints[index]));

    }
    IEnumerator PatrolRest(Transform targetPos)
    {
        enemyController.enemyProperties.objectRigid.velocity = Vector3.zero;

        yield return new WaitForSeconds(enemyController.enemyProperties.patrolRestTime);

        target = targetPos;
    }
}
