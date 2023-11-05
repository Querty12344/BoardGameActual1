using UnityEngine;

namespace DefaultNamespace.GameStates
{
    public class MainMenuState:IState
    {
        private readonly IUIService _uiService;
        private IStateMachine _stateMachine;
        
        public MainMenuState(IUIService uiService)
        {
            _uiService = uiService;
        }

        public void Enter(IStateMachine stateMachine)
        {
            Debug.Log("menu");
            _stateMachine = stateMachine;
            _uiService.HideLoadingCurtain();
            _uiService.ShowMainMenu(StartGame);
        }

        private void StartGame()
        {
            _uiService.ShowLoadingCurtain();
            _stateMachine.EnterState<LoadLevelState>();
        }
        
    }
}