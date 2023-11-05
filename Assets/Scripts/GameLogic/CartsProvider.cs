using System;
using System.Collections.Generic;
using System.Linq;
using Services;
using Services.Factories;
using UnityEngine;
using Random = System.Random;

namespace GameLogic
{
    class CartsProvider : ICartsProvider
    {
        private List<Cart> _carts;
        private readonly IGameFactory _gameFactory;
        private readonly IPlayersHandler _players;

        public CartsProvider(IPlayersHandler players, IGameFactory gameFactory)
        {
            _players = players;
            _gameFactory = gameFactory;
        }

        public void InitCarts()
        {
            _carts?.Clear();
            _carts = _gameFactory.CreateCarts().Select(c=> new {C=c, sort= Randomizer.rng.Next()})
                .OrderBy (c =>c.sort).Select(c=>c.C).ToList();
        }

        public void RemoveAllCarts()
        {
            foreach (var cart in _carts)
            {
                cart.Remove();
            }
            _carts.Clear();
        }

        public void FillWithRandomCarts(List<ICartsHandler> cartsHandler, IPlayer first, bool firstIncluded)
        {
            IPlayer active = first;
            
            for(int i = 0;i< _players.Players.Count;i++)
            {
                if (!firstIncluded)
                {
                    active = _players.GetNextPlayer(active);
                    i++;
                }
                int cartsToAddCount = active.CartsHandler.CartsToAddCount();
                List<Cart> cartsToAdd = new List<Cart>();
                if (cartsToAddCount > _carts.Count)
                {
                    cartsToAdd = _carts;
                    _carts.Clear();
                }
                else
                { 
                    cartsToAdd = _carts.Take(cartsToAddCount).ToList();
                    _carts.RemoveRange(0,cartsToAddCount);
                }
                active.CartsHandler.AddCarts(cartsToAdd,active,active == _players.MainPlayer);
                active = _players.GetNextPlayer(active);
                
            }
        }

        public Cart SetTrumpSuit()
        {
            Cart trumpCart = _carts[0];
            _carts.RemoveAt(0);
            return trumpCart;
        }

        public bool HasNoCarts() => _carts.Count == 0;
    }
}