using GameLogic;
using GameStates;
using Services;
using Services.Factories;
using StaticData;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.GameStates
{
    public class LoadLevelState:IState
    {
        private readonly IPlayersHandler _playersHandler;
        private readonly ICartsProvider _cartsProvider;
        private readonly GameTableLayout _gameTableLayout;
        private readonly IGameFactory _gameFactory;
        private readonly ISceneLoader _sceneLoader;
        private readonly PerformanceSettings _performanceSettings;
        private IInputService _inputService;
        private readonly ICartDragger _cartDragger;
        private IStateMachine _stateMachine;

        public LoadLevelState(IPlayersHandler playersHandler,ICartsProvider cartsProvider, GameTableLayout gameTableLayout
            , IGameFactory gameFactory, ISceneLoader sceneLoader, ISettingsProvider performanceSettings, IInputService inputService,
            ICartDragger cartDragger)
        {
            _playersHandler = playersHandler;
            _cartsProvider = cartsProvider;
            _gameTableLayout = gameTableLayout;
            _gameFactory = gameFactory;
            _sceneLoader = sceneLoader;
            _inputService = inputService;
            _cartDragger = cartDragger;
            _performanceSettings = performanceSettings.GetPerformanceSettings();
        }
        public void Enter(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _sceneLoader.Load(SceneNames.Game,OnGameSceneLoaded);
        }

        private void OnGameSceneLoaded()
        {
            InitServices();
            _stateMachine.EnterState<GameLoopState>();
        }
        private void InitServices()
        {
            _inputService.Init();
            PositionsHandler positionsHandler = GameObject.FindObjectOfType<PositionsHandler>();
            GameObject.FindObjectOfType<GameTableZone>().Init(_cartDragger);
            GlobalGradient.Init(_performanceSettings.GradientSpeed,_performanceSettings.GradientLerpSpeed);
            _gameTableLayout.Init(positionsHandler);
            _gameFactory.Init(positionsHandler);
            _cartsProvider.InitCarts();
            _playersHandler.InitPlayers(1);
        }
        
    }
}