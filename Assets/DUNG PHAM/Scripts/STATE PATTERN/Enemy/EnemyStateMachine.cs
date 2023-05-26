using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : AStateMachine
{
    [HideInInspector] public EnemyAnimation enemyAnimation;
    [HideInInspector] public EnemyController enemyController;
    public EnemyIdleState idleState;
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;
    public EnemyHurtState hurtState;
    public EnemyDieState dieState;

    void StateDeclaration()
    {
        idleState = new EnemyIdleState(this);
        patrolState = new EnemyPatrolState(this);
        chaseState = new EnemyChaseState(this);
        attackState = new EnemyAttackState(this);
        hurtState = new EnemyHurtState(this);
        dieState = new EnemyDieState(this);
    }


    void Awake()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyController = GetComponent<EnemyController>();

        StateDeclaration();
    }
    void OnEnable()
    {
        currentState = idleState;
        currentState.EnterState();
    }
}

