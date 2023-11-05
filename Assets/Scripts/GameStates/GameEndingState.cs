namespace DefaultNamespace.GameStates
{
    public class GameEndingState:IState
    {
        private IStateMachine _stateMachine;

        public void Enter(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}