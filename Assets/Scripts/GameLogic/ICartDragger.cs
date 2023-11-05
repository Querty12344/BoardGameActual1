using System;

namespace GameLogic
{
    public interface ICartDragger
    {
        event Action OnDraggingStarted;
        event Action OnDraggingEnded;
        void CartExitTable(Cart cart);
        void CartEnteredTable(Cart cart);
        void Activate(IPlayer player);
        void Deactivate();
        void Clear();
    }
}