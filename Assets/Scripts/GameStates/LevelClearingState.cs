using DefaultNamespace;
using DefaultNamespace.GameStates;
using GameLogic;

namespace GameStates
{
    public class LevelClearingState:IState
    {
        private ICoroutineRunner _coroutineRunner;
        private readonly IPlayersHandler _playersHandler;
        private readonly IGameLoop _gameLoop;
        private readonly ICartsProvider _cartsProvider;
        private readonly ICartDragger _cartDragger;


        public LevelClearingState(ICoroutineRunner coroutineRunner,IPlayersHandler playersHandler,IGameLoop gameLoop,ICartsProvider cartsProvider,
            ICartDragger cartDragger)
        {
            _coroutineRunner = coroutineRunner;
            _playersHandler = playersHandler;
            _gameLoop = gameLoop;
            _cartsProvider = cartsProvider;
            _cartDragger = cartDragger;
        }

        public void Enter(IStateMachine stateMachine)
        {
            _cartDragger.Clear();
            _coroutineRunner.StopAllCoroutines();
            _gameLoop.StopLoop();
            _playersHandler.RemovePlayers();
            _cartsProvider.RemoveAllCarts();
        }
    }
}