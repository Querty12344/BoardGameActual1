using System;
using System.Collections.Generic;
using DefaultNamespace.GameStates;
using GameStates;

namespace DefaultNamespace
{
    public class StateMachine : IStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _activeState;
        
        public StateMachine(BootstrapState bootstrapState,
            DataLoadingState dataLoadingState,
            GameEndingState gameEndingState,
            GameLoopState gameLoopState,
            LoadLevelState loadLevelState,
            MainMenuState mainMenuState,
            LevelClearingState levelClearingState)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(DataLoadingState)] = dataLoadingState,
                [typeof(GameEndingState)] = gameEndingState,
                [typeof(GameLoopState)] = gameLoopState,
                [typeof(LoadLevelState)] = loadLevelState,
                [typeof(MainMenuState)] = mainMenuState,
                [typeof(LevelClearingState)] = levelClearingState
            };
        }

        public void EnterState<TState>() where TState : IState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.Enter(this);
        }
    }
}