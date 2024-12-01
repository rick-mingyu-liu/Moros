// BossStateMachine.cs
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    private BossState currentState;

    public void ChangeState(BossState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter();
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
