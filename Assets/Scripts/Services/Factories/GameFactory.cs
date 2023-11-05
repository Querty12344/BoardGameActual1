using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using GameLogic;
using Unity.Mathematics;
using UnityEngine;

namespace Services.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ICartDragger _cartDragger;
        private readonly ISettingsProvider _settingsProvider;
        private PositionsHandler _positions;
        private readonly ICartSkinProvider _skinProvider;
        private IInputService _inputService;
        private IGameTable _gameTable;
        private ICoroutineRunner _coroutineRunner;

        public GameFactory(IAssetProvider assetProvider, ICartDragger cartDragger, ISettingsProvider settingsProvider,
            ICartSkinProvider skinProvider, IInputService inputService, IGameTable gameTable, ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _cartDragger = cartDragger;
            _settingsProvider = settingsProvider;
            _skinProvider = skinProvider;
            _inputService = inputService;
            _gameTable = gameTable;
            _coroutineRunner = coroutineRunner;
        }

        public void Init(PositionsHandler positionsHandler)
        {
            _positions = positionsHandler;
        }

        public List<Cart> CreateCarts()
        {
            List<Cart> carts = new List<Cart>();
            Cart cartPrefab = _assetProvider.GetCart();
            int cartsCount = _settingsProvider.GetCartData().CartForEachSuitCount *
                             _settingsProvider.GetCartData().Suits.Length;
            int nominal = 0;
            int suit = 0;
            for (int i = 0; i < cartsCount; i++)
            {
                Cart cart = GameObject.Instantiate(cartPrefab, _positions.CartSpawn.position, quaternion.identity);
                cart.Construct(nominal, suit);
                cart.Mover.Construct(_settingsProvider.GetPerformanceSettings().CartMovingSpeed,
                    _settingsProvider.GetPerformanceSettings().CartRotSpeed,_cartDragger);
                cart.View.Construct(_skinProvider,nominal, suit);
                carts.Add(cart);
                nominal++;
                if (nominal == _settingsProvider.GetCartData().CartForEachSuitCount)
                {
                    nominal = 0;
                    suit++;
                }
            }

            return carts;
        }

        public IPlayer CreatePlayer()
        {
            ICartsHandler cartsHandler = new CartsHandler(CreateHandLayout());
            IPlayer player = new Player(cartsHandler, CreatePlayerView(), _cartDragger);
            cartsHandler.SetLayoutTransform(player.GetHandTransform());
            return player;
        }

        public IPlayer CreateBot()
        {
            ICartsHandler cartsHandler = new CartsHandler(CreateHandLayout());
            IPlayer bot = new Bot(cartsHandler, CreatePlayerView(),_gameTable,_settingsProvider,_coroutineRunner);
            cartsHandler.SetLayoutTransform(bot.GetHandTransform());
            return bot;
        }

        private PlayerHandLayout CreateHandLayout()
        {
            PlayerHandLayout handLayout = GameObject.Instantiate(_assetProvider.GetHandLayout());
            handLayout.Construct(_settingsProvider.GetPerformanceSettings(),_inputService);
            return handLayout;
        }

        private PlayerView CreatePlayerView()
        {
            Transform playerViewPos = _positions.GetNextPlayerViewPosition();
            return GameObject.Instantiate(_assetProvider.GetDefaultPlayerView(), playerViewPos);
        }
    }
}