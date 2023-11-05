using StaticData;
using UnityEngine;

namespace DefaultNamespace.GameStates
{
    public class BootstrapState:IState
    {
        private readonly ISceneLoader _sceneLoader;
        private IStateMachine _stateMachine;

        public BootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        public void Enter(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _sceneLoader.Load(SceneNames.Boot,OnBootSceneLoaded);
        }

        private void OnBootSceneLoaded()
        {
            _stateMachine.EnterState<DataLoadingState>();
        }
    }
}