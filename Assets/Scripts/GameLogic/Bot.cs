using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Newtonsoft.Json.Serialization;
using Services;
using StaticData;
using UnityEngine;

namespace GameLogic
{
    public class Bot:IPlayer
    {
        private readonly PlayerView _playerView;
        private readonly IGameTable _gameTable;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly PerformanceSettings _settings;


        public Bot(ICartsHandler cartsHandler, PlayerView playerView, IGameTable gameTable,ISettingsProvider settingsProvider,ICoroutineRunner coroutineRunner)
        {
            _settings = settingsProvider.GetPerformanceSettings();
            CartsHandler = cartsHandler;
            _playerView = playerView;
            _gameTable = gameTable;
            _coroutineRunner = coroutineRunner;
        }
        
        public ICartsHandler CartsHandler { get; }
        public void ActivateDefuseState()
        {
            _playerView.SetActiveMarker(Marker.Disable);
            _coroutineRunner.StartCoroutine(WaitForSeconds(_settings.BotWaitingTime, GiveAttackCart));
        }

        public void ActivateAttackState()
        {
            _playerView.SetActiveMarker(Marker.Disable);
            _coroutineRunner.StartCoroutine(WaitForSeconds(_settings.BotWaitingTime, GiveDefuseCart));
        }

        public void Deactivate()
        {
            _playerView.SetActiveMarker(Marker.Disable);
        }
        
        
        public void Remove()
        {
            GameObject.Destroy(_playerView);
        }

        public Transform GetHandTransform() => _playerView.GetHandTransform();

        private void GiveAttackCart()
        {
            bool cartAccepted = false;
            List<Cart> cartsSorted = CartsHandler.Carts.OrderBy(c => c.GetNominal() * (c.GetSuit() == _gameTable.GetTrampSuit() ? 1000 : 1)).ToList();
            foreach (var c in cartsSorted)
            {
                if(_gameTable.TryAddCart(c))
                {
                    cartAccepted = true;
                    break;
                }
            }

            if (!cartAccepted)
            {
                _gameTable.Pass();
            }
        }

        private void GiveDefuseCart()
        {
            bool cartAccepted = false;
            List<Cart> cartsSorted = CartsHandler.Carts.OrderBy(c => c.GetNominal() * (c.GetSuit() == _gameTable.GetTrampSuit() ? 1000 : 1)).ToList();
            foreach (var c in cartsSorted)
            {
                if(_gameTable.TryAddCart(c))
                {
                    cartAccepted = true;
                    break;
                }
            }

            if (!cartAccepted)
            {
                _gameTable.Pass();
            }
        }

        private IEnumerator WaitForSeconds(int seconds,Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback.Invoke();
        }
    }
}