using UnityEngine;

namespace Services
{
    public interface ICartSkinProvider
    {
        Sprite GetCartFace();
        Sprite GetCartBack(); 
        Sprite GetSuit(int suit);
        Color[] GetSuitColors(int suit);
        Sprite GetEmptySprite();
    }
}