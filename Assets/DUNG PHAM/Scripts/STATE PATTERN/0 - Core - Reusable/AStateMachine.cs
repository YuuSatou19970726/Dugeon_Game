using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStateMachine : MonoBehaviour
{
    public IState currentState;
    public IState previousState;
    bool inTransition = false;

    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/
    public void Update()
    {
        if (currentState != null && !inTransition)
            currentState.UpdateState();
    }

    public void FixedUpdate()
    {
        if (currentState != null && !inTransition)
            currentState.FixedUpdateState();
    }

    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/
    public void SwitchState(IState newState)
    {
        if (currentState == newState || inTransition)
            return;

        ChangeStateRoutine(newState);
    }

    void ChangeStateRoutine(IState newState)
    {
        inTransition = true;

        if (currentState != null)
            currentState.ExitState();

        if (previousState != null)
            previousState = currentState;

        currentState = newState;

        if (currentState != null)
            currentState.EnterState();

        inTransition = false;
    }

    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/
    public void RevertState()
    {
        if (previousState != null)
            SwitchState(previousState);
    }
    /***************************************************************************************************************************************/
    /***************************************************************************************************************************************/

    public void DisplayCurrentState()
    {
        Debug.Log(currentState);
    }
}
