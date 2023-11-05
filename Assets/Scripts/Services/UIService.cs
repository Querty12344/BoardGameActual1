using System;
using DefaultNamespace;
using Services.Factories;
using UIElements;
using UnityEngine;

namespace Services
{
    public class UIService : IUIService
    {
        private MainMenu _mainMenu;
        private GameHud _hud;
        private LoadingCurtain _loadingCurtain;
        private EndGameWindow _endGameWindow;
        private IUIFactory _factory;

        public UIService(IUIFactory factory)
        {
            _factory = factory;
        }

        public void ShowMainMenu(Action startGame)
        {
            RemoveUI();
            _mainMenu = _factory.CreateMainMenu(startGame);
            
        }
        
        public void ShowGameHud(Action exitGame)
        {
            RemoveUI();
            _hud = _factory.CreateGameHud(exitGame);
        }

        public void ShowLoadingCurtain()
        {
            RemoveUI();
            _loadingCurtain = _factory.CreateLoadingCurtain();
            GameObject.DontDestroyOnLoad(_loadingCurtain);
        }

        public void HideLoadingCurtain()
        {
            if (_loadingCurtain == null)
            {
                return;
            }
            _loadingCurtain.Remove();
        }

        public void ShowEndGameWindow(bool activePlayerWon,Action restartGame,Action exitGame)
        {
            RemoveUI();
            _endGameWindow = _factory.CreateEndGameWindow(activePlayerWon,restartGame,exitGame);
        }

        private void RemoveUI()
        {
            if (_mainMenu != null)
            {
                _mainMenu.Remove();
                _hud = null;
            }

            if (_hud != null)
            {
                _hud.Remove();
                _hud = null;
            }
            if (_endGameWindow != null)
            {
                _endGameWindow.Remove();
                _endGameWindow = null;
            }
        }
    }
}