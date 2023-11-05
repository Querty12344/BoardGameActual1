using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public interface ICartsHandler
    {
        public void RemoveCart(Cart cart);
        void AddCarts(List<Cart> carts,IPlayer owner,bool isMainPlayer);
        int CartsToAddCount();
        void SetLayoutTransform(Transform handTransform);
        List<Cart> Carts { get; }
    }
}