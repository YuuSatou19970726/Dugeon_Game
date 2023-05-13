using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [HideInInspector] public EnemyAnimation enemyAnimation;
    [HideInInspector] public EnemyController enemyController;
    EnemyBaseState currentState;
    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyAttackState attackState = new EnemyAttackState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyHurtState hurtState = new EnemyHurtState();
    public EnemyPatrolState patrolState = new EnemyPatrolState();

    void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyController = GetComponent<EnemyController>();
    }
    void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);

    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        if (state != currentState)
            currentState.ExitState(this);


        currentState = state;

        currentState.EnterState(this);

        // Debug.Log(currentState);
    }
}

