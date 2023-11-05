using System;

namespace GameLogic
{
    public interface IGameTable
    {
        void Pass();
        void SetTrumpSuit(Cart cart);
        void SetState(IPlayer activePlayer,bool isAttack,Action acceptCart,Action pass);
        bool TryAddCart(Cart cart);
        Cart[] ClearTable();
        int GetTrampSuit();

    }
}