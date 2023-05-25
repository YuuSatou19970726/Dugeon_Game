using UnityEngine;

public abstract class StateMachineMB : MonoBehaviour
{
    public IState currentState;
    public IState _previousState;

    bool _inTransition = false;


    // pass down Update ticks to States, since they won't have a MonoBehaviour
    public void Update()
    {
        // simulate update ticks in states
        if (currentState != null && !_inTransition)
            currentState.UpdateState();
    }

    public void FixedUpdate()
    {
        // simulate fixedUpdate ticks in states
        if (currentState != null && !_inTransition)
            currentState.FixedUpdateState();
    }

    public void SwitchState(IState newState)
    {
        if (currentState == newState || _inTransition)
            return;

        ChangeStateRoutine(newState);
    }

    void ChangeStateRoutine(IState newState)
    {
        _inTransition = true;

        if (currentState != null)
            currentState.ExitState();

        if (_previousState != null)
            _previousState = currentState;

        currentState = newState;

        if (currentState != null)
            currentState.EnterState();

        _inTransition = false;
    }
    public void RevertState()
    {
        if (_previousState != null)
            SwitchState(_previousState);
    }

}