using System.Collections.Generic;
using GameLogic;
using UnityEngine;

namespace Services.Factories
{
    public class CartsHandler : ICartsHandler
    {
        private PlayerHandLayout _handLayout;
        public List<Cart> Carts { get; private set; }

        public CartsHandler(PlayerHandLayout handLayout)
        {
            _handLayout = handLayout;
            Carts = new List<Cart>();
        }

        public void RemoveCart(Cart cart)
        {
            cart.Owner = null;
            Carts.Remove(cart);
            _handLayout.RemoveCart(cart);
        }

        public void AddCarts(List<Cart> carts,IPlayer owner,bool isMainPlayer)
        {
            Carts.AddRange(carts);
            foreach (var cart in carts)
            {
                _handLayout.AddCart(cart);
                cart.Owner = owner;
                if (isMainPlayer)
                {
                    cart.View.Activate();
                }
                else
                {
                    cart.View.Deactivate();
                }
            }
        }

        public int CartsToAddCount()
        {
            if (Carts.Count >= 6)
            {
                return 0;
            }
            return 6 - Carts.Count;
        }

        public void SetLayoutTransform(Transform handTransform)
        {
            _handLayout.transform.position = handTransform.position;
            _handLayout.transform.SetParent(handTransform);
        }
    }
}