using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateMachine enemy);
    public abstract void UpdateState(EnemyStateMachine enemy);
    public abstract void FixedUpdateState(EnemyStateMachine enemy);
    public abstract void ExitState(EnemyStateMachine enemy);
}
