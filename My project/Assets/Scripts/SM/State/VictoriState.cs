using SM;

internal class VictoriState : IState {
    private IState _stateImplementation;

    public VictoriState(StateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }

    public void Enter()
    {
        _stateImplementation.Enter();
    }

    public void Update()
    {
        _stateImplementation.Update();
    }

    public void Exit()
    {
        _stateImplementation.Exit();
    }
}