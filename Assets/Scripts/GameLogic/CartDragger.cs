using System;
using System.Collections;
using DefaultNamespace;
using ModestTree.Util;
using Services;
using StaticData;
using TMPro;
using UnityEngine;

namespace GameLogic
{
    public class CartDragger : ICartDragger
    {
        private bool _isActive;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IInputService _inputService;
        private readonly IGameTable _gameTable;
        private Cart _activeCart;
        private Cart _previousCart;
        private IPlayer _player;
        private bool _isOnDrag;
        private readonly PerformanceSettings _settings;
        public event Action OnDraggingStarted;
        public event Action OnDraggingEnded;

        public CartDragger(ICoroutineRunner coroutineRunner,IInputService inputService,ISettingsProvider settingsProvider,IGameTable gameTable)
        {
            _settings = settingsProvider.GetPerformanceSettings();
            _coroutineRunner = coroutineRunner;
            _inputService = inputService;
            _gameTable = gameTable;
        }

        public void Activate(IPlayer player)
        {
            _player = player;
            _isActive = true;
            _inputService.MouseStartDragCart += StartDragCart;
            _inputService.MouseStopDragCart += StopDragCart;
            _coroutineRunner.StartCoroutine(Drag());
        }

        public void Deactivate()
        {
            _activeCart = null;
            _inputService.MouseStartDragCart -= StartDragCart;
            _inputService.MouseStopDragCart -= StopDragCart;
            _isActive = false;
        }

        public void Clear()
        {
            Deactivate();
            OnDraggingStarted = null;
            OnDraggingEnded = null;
        }

        private void StartDragCart(Cart cart)
        {
            if (cart.Owner == _player && _player != null)
            { 
                OnDraggingStarted?.Invoke();
                _activeCart?.Mover.StopDrag();
                _activeCart = cart;
                _activeCart.Mover.StartDrag();   
            }
        }

        private void StopDragCart()
        {
            if (_activeCart != null)
            {
                OnDraggingEnded?.Invoke();
                _activeCart.Mover.StopDrag();
                _previousCart = _activeCart;
            }

            _activeCart = null;
        }

        public void CartEnteredTable(Cart cart)
        {
            if (cart == _previousCart)
            {
                _gameTable.TryAddCart(_previousCart);
            }
        }

        public void CartExitTable(Cart cart)
        {
            if (cart == _previousCart)
            {
                _gameTable.TryAddCart(_previousCart);
            }
        }

        private IEnumerator Drag()
        {
            while (true)
            {
                if (_isActive && _activeCart != null)
                {
                    _activeCart.Mover.SetPosition(_inputService.GetMousePosition() + _settings.CartDragOffset);
                    _activeCart.Mover.SetRotation(Quaternion.Euler(90,0,0));
                }
                yield return new WaitForFixedUpdate();
            }
        }
        
    }
}