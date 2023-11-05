using System.Collections.Generic;
using Services;
using UnityEngine;

namespace GameLogic
{
    public class GameTableLayout
    {
        private PositionsHandler _positions;
        private int _lastCartOrder;
        private Vector3 _randomOffset;
        public void Init(PositionsHandler positionsHandler)
        {
            _positions = positionsHandler;
            _lastCartOrder = 1;
        }
        public void RemoveOutOfTable(List<Cart> carts)
        {
            _randomOffset = _positions.GetMaxOnTableOffset();
            _randomOffset.z *= ((float)Randomizer.rng.NextDouble()) * Randomizer.rng.Next(-1,1)*0.5f;
            _randomOffset.x *= ((float)Randomizer.rng.NextDouble()) * Randomizer.rng.Next(-1,1)*0.5f;
            carts.ForEach(c => c.Mover.SetPosition(_positions.GetOutPos()+_randomOffset));
            carts.ForEach(c => c.View.Deactivate());
        }
        
        public void Add(Cart cart,bool isAttack)
        {
            if (isAttack)
            {
                _randomOffset = _positions.GetMaxOnTableOffset();
                _randomOffset.z *= ((float)Randomizer.rng.NextDouble() + 0.5f) * Randomizer.rng.Next(-1,1);
                _randomOffset.x *= ((float)Randomizer.rng.NextDouble() + 0.5f) * Randomizer.rng.Next(-1,1);
            }
            else
            {
                _randomOffset += _positions.GetNextCartOnTableOffset();
            }
            cart.Mover.SetPosition(_positions.OnTablePos.position + _randomOffset);
            cart.Mover.SetRotation(Quaternion.Euler(90,0,0));
            cart.GetComponentInChildren<Canvas>().sortingOrder = _lastCartOrder;
            cart.View.Activate();
            _lastCartOrder++;
        }

        public void SetTrumpCart(Cart cart)
        {
            cart.View.Activate();
            cart.Mover.SetPosition(_positions.GetTrumpPos());
        }
    }
}