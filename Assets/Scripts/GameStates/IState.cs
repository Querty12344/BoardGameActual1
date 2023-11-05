namespace DefaultNamespace
{
    public interface IState
    {
        void Enter(IStateMachine stateMachine);

        void Exit()
        {
        }
    }
}