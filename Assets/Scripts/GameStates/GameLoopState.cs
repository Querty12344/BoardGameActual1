using DefaultNamespace;
using DefaultNamespace.GameStates;
using GameLogic;
using StaticData;
using UnityEngine;
using YG;

namespace GameStates
{
    public class GameLoopState : IState
    {
        private readonly IGameLoop _gameLoop;
        private readonly IUIService _uiService;
        private readonly ISceneLoader _sceneLoader;
        private IStateMachine _stateMachine;
        
        public GameLoopState(IGameLoop gameLoop,IUIService uiService,ISceneLoader sceneLoader)
        {
            _gameLoop = gameLoop;
            _uiService = uiService;
            _sceneLoader = sceneLoader;
        }

        public void Enter(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _uiService.HideLoadingCurtain();
            _uiService.ShowGameHud(ExitGame);
            _gameLoop.StartLoop(GameEnded);
        }

        private void GameEnded(bool activePlayerWon)
        {
            _uiService.ShowEndGameWindow(activePlayerWon,RestartGame,ExitGame);
        }

        private void RestartGame()
        {
            GameObject.FindObjectOfType<YandexGame>()._FullscreenShow();
            _stateMachine.EnterState<LevelClearingState>();
            _stateMachine.EnterState<LoadLevelState>();
        }
        private void ExitGame()
        {
            GameObject.FindObjectOfType<YandexGame>()._FullscreenShow();
            _uiService.ShowLoadingCurtain();
            _stateMachine.EnterState<LevelClearingState>();
            _sceneLoader.Load(SceneNames.Menu,_stateMachine.EnterState<MainMenuState>);
        }
    }
}