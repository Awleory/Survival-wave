
public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startState)
    {
        CurrentState = startState;
        CurrentState?.Enter();
    }

    public void SetState(State state)
    {
        CurrentState?.Exit();

        CurrentState = state;
        CurrentState?.Enter();
    }
}
