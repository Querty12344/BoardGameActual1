using Services;
using StaticData;
using UnityEngine;

namespace DefaultNamespace.GameStates
{
    public class DataLoadingState:IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetProvider _assetProvider;
        private IStateMachine _stateMachine;
        private readonly IUIService _uiService;

        public DataLoadingState(ISceneLoader sceneLoader,IAssetProvider assetProvider,IUIService uiService)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _uiService = uiService;
        }

        public void Enter(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _assetProvider.LoadAssets(OnDataLoaded);
        }

        private void OnDataLoaded()
        {
            _uiService.ShowLoadingCurtain();
            _sceneLoader.Load(SceneNames.Menu,OnMenuSceneLoaded);
        }

        private void OnMenuSceneLoaded()
        {
            _stateMachine.EnterState<MainMenuState>();
        }
    }
}