using GameLogic;
using UnityEngine;

namespace Services
{
    
    class CartSkinProvider : ICartSkinProvider
    {
        private CartsData _cartsData;
        public int ActiveStyle { private get; set; }

        public CartSkinProvider(ISettingsProvider settings)
        {
            _cartsData = settings.GetCartData();
        }

        public Sprite GetCartFace() => _cartsData.Faces[ActiveStyle];
        public Sprite GetCartBack() => _cartsData.Backs[ActiveStyle];
        public Sprite GetSuit(int suit) => _cartsData.SuitSprites[suit];
        public Color[] GetSuitColors(int suit) => _cartsData.SuitColors[suit].Colors;
        public Sprite GetEmptySprite() => _cartsData.EmptySprite;
    }
}