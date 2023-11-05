using System;
using DefaultNamespace;
using GameLogic;
using ModestTree.Util;
using UIElements;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private IAssetProvider _assetProvider;
        private readonly IGameTable _gameTable;

        public UIFactory(IAssetProvider assetProvider,IGameTable gameTable)
        {
            _assetProvider = assetProvider;
            _gameTable = gameTable;
        }

        public MainMenu CreateMainMenu(Action startGame)
        { 
            MainMenu menu = GameObject.Instantiate(_assetProvider.GetMainMenu());
            menu.Construct(startGame);
            return menu;
        }

        public GameHud CreateGameHud(Action exitGame)
        { 
            GameHud hud = GameObject.Instantiate(_assetProvider.GetGameHud());
            hud.Construct(exitGame,_gameTable.Pass);
            return hud;
        }

        public LoadingCurtain CreateLoadingCurtain()
        {
            return GameObject.Instantiate(_assetProvider.GetLoadingCurtain());
        }

        public EndGameWindow CreateEndGameWindow(bool playerWon, Action restartGame, Action exitGame)
        {
            EndGameWindow endGameWindow = GameObject.Instantiate(_assetProvider.GetEndGameWindow());
            endGameWindow.Construct(playerWon,restartGame,exitGame);
            return endGameWindow;
        }
    }
}