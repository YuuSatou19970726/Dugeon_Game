using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    DragonWarriorController dragonWarriorController;

    [SerializeField]
    Transform leftEdge, rightEdge;

    [Header("Enemy")]
    [SerializeField]
    Transform enemy;

    Vector3 initScale;
    bool movingLeft;

    //Idle Behaviour
    float idleDuration = 3f;
    float idleTimer;

    bool isDisable = false;

    private void Awake()
    {
        dragonWarriorController = FindAnyObjectByType<DragonWarriorController>();
        initScale = enemy.localScale;
    }

    float speed = 2f;

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy != null && !isDisable)
            {
                if (enemy.position.x >= leftEdge.position.x)
                {
                    dragonWarriorController.GetDragonWarriorMove();
                    MoveInDirection(-1);
                }
                else
                {
                    dragonWarriorController.GetPlayerIdle();
                    DirectionChange();
                }
            }
        } else
        {
            if (enemy != null && !isDisable)
            {
                if (enemy.position.x <= rightEdge.position.x)
                {
                    dragonWarriorController.GetDragonWarriorMove();
                    MoveInDirection(1);
                }
                else
                {
                    dragonWarriorController.GetPlayerIdle();
                    DirectionChange();
                }
            }
        }
    }

    private void OnDisable()
    {
        isDisable = true;
    }

    void DirectionChange()
    {
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    void MoveInDirection (int direction)
    {
        idleTimer = 0f;
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
