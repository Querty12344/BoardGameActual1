using System;
using UnityEngine;

namespace GameLogic
{
    public class GameTableZone:MonoBehaviour
    {
        private ICartDragger _cartDragger;


        public void Init(ICartDragger cartDragger)
        {
            _cartDragger = cartDragger;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Cart>(out var cart))
            {

                _cartDragger.CartEnteredTable(cart);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<Cart>(out var cart))
            {
                _cartDragger.CartExitTable(cart);
            }
        }

    }
}