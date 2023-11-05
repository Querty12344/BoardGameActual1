using System;
using GameLogic;
using Services;
using StaticData;
using UIElements;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    class AssetProvider : IAssetProvider
    {
        private PlayerHandLayout _playerHandLayout;
        private PlayerView _player;
        private Cart _cart;
        private MainMenu _mainMenu;
        private GameHud _hud;
        private LoadingCurtain _loadingCurtain;
        private EndGameWindow _endGameWindow;

        public void LoadAssets(Action callBack)
        {
            _endGameWindow = Resources.Load<EndGameWindow>(ResourcePath.EndGameWindow);
            _playerHandLayout = Resources.Load<PlayerHandLayout>(ResourcePath.PlayerHand);
            _player = Resources.Load<PlayerView>(ResourcePath.StandardPlayer);
            _cart = Resources.Load<Cart>(ResourcePath.Cart);
            _mainMenu = Resources.Load<MainMenu>(ResourcePath.MainMenu);
            _hud = Resources.Load<GameHud>(ResourcePath.GameHud);
            _loadingCurtain = Resources.Load<LoadingCurtain>(ResourcePath.LoadingCurtain);
            callBack.Invoke();
        }

        public PlayerHandLayout GetHandLayout() => _playerHandLayout;
        public PlayerView GetDefaultPlayerView() => _player;
        public Cart GetCart() => _cart;
        public MainMenu GetMainMenu() => _mainMenu;
        public GameHud GetGameHud() => _hud;
        public LoadingCurtain GetLoadingCurtain() => _loadingCurtain;
        public EndGameWindow GetEndGameWindow() => _endGameWindow;
    }
}