namespace DefaultNamespace
{
    public interface IStateMachine
    {
        public void EnterState<TState>() where TState : IState;
    }
}