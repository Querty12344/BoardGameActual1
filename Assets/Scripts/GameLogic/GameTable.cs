using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLogic
{
    public class GameTable : IGameTable
    {
        private readonly GameTableLayout _tableLayout;
        private List<Cart> _cartsOnTable;
        private Cart _attackCart;
        private int _trumpSuit;
        private IPlayer _activePlayer;
        private bool _isAttackState;
        private Action _onCartAccepted;
        private Action _pass;

        

        public GameTable(GameTableLayout tableLayout)
        {
            _tableLayout = tableLayout;
            _cartsOnTable = new List<Cart>();
        }

        public int GetTrampSuit() => _trumpSuit;

        public void SetTrumpSuit(Cart cart)
        {
            _trumpSuit = cart.GetSuit();
            cart.Owner = null;
            _tableLayout.SetTrumpCart(cart);
        }

        public void SetState(IPlayer activePlayer, bool isAttackState,Action cartAccepted,Action pass)
        {
            _pass = pass;
            _activePlayer = activePlayer;
            _isAttackState = isAttackState;
            _onCartAccepted = cartAccepted;
        }

        public void Pass()
        {
            _pass?.Invoke();
        }

        public Cart[] ClearTable()
        {
            Cart[] carts = _cartsOnTable.ToArray();
            _tableLayout.RemoveOutOfTable(_cartsOnTable);
            _cartsOnTable.Clear();
            _attackCart = null;
            Debug.Log($"карт было на столе {carts.Length} ");
            return carts;
        }

        public bool TryAddCart(Cart cart)
        {
            if (cart.Owner != _activePlayer)
            {
                return false;
            }
            if (_isAttackState)
            {
                if (TrySetAttackCart(cart))
                {
                    AcceptCart(cart);
                    return true;
                }
                return false;
            }
            if (TryAddDefuseCart(cart))
            {
                AcceptCart(cart);
                return true;
            }
            return false;
        }

        private void AcceptCart(Cart cart)
        {
            _cartsOnTable.Add(cart);
            cart.Owner.CartsHandler.RemoveCart(cart);
            _tableLayout.Add(cart,_isAttackState);
            _onCartAccepted.Invoke();
        }

        private bool TrySetAttackCart(Cart cart)
        {
            if (_attackCart != null)
            {
                return false;
            }
            if (_cartsOnTable.Count == 0 || _cartsOnTable.Count(c => c.GetNominal() == cart.GetNominal()) > 0)
            {
                _attackCart = cart;
                return true;
            }
            return false;
        }

        private bool TryAddDefuseCart(Cart cart)
        {
            if (_attackCart == null)
            {
                return false;
            }

            if (IsStronger(cart, _attackCart))
            {
                _attackCart = null;
                return true;
            }

            return false;
        }

        private bool IsStronger(Cart a,Cart b)
        {
            if (a.GetSuit() == _trumpSuit)
            {
                if (b.GetSuit() == _trumpSuit && b.GetNominal() >= a.GetNominal())
                {
                    return false;
                }
                return true;
            }
            if (b.GetSuit() == _trumpSuit)
            {
                return false;
            }

            if (a.GetSuit() != b.GetSuit())
            {
                return false;
            }
            return a.GetNominal() > b.GetNominal();
        }
    }
}