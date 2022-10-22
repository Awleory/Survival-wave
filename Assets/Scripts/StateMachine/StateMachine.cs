using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _currentState;

    public State CurrentState => _currentState;

    public void Initialize(State startState)
    {
        _currentState = startState;
        _currentState?.Enter();
    }

    public void SetState(State state)
    {
        _currentState?.Exit();

        _currentState = state;
        _currentState?.Enter();
    }
}
