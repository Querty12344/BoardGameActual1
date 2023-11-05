using System;
using UnityEngine;

namespace GameLogic
{
    public interface IInputService
    {
        event Action<Cart> MouseStartDragCart;
        event Action<Cart> OnMouseEnterCart;
        event Action MouseStopDragCart;
        Vector3 GetMousePosition();
        void Init();
    }
}