using UnityEngine;

namespace GameLogic
{
    public class Player:IPlayer
    {
        public ICartsHandler CartsHandler { get; }
        private PlayerView _playerView;
        private readonly ICartDragger _cartDragger;

        public Player(ICartsHandler cartsHandler, PlayerView playerView,ICartDragger cartDragger)
        {
            _playerView = playerView;
            _cartDragger = cartDragger;
            CartsHandler = cartsHandler;
        }

        public void ActivateDefuseState()
        {
            _playerView.SetActiveMarker(Marker.Defuse);
            _cartDragger.Activate(this);
        }

        public void ActivateAttackState()
        {
            _playerView.SetActiveMarker(Marker.Attack);
            _cartDragger.Activate(this);
        }

        public void Deactivate()
        {
            _playerView.SetActiveMarker(Marker.NotActive);
            _cartDragger.Deactivate();
        }

        public void Remove()
        {
            GameObject.Destroy(_playerView);
            _cartDragger.Deactivate();
        }

        public Transform GetHandTransform() => _playerView.GetHandTransform();
        
    }
}