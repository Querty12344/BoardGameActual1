using UnityEngine;

namespace GameLogic
{
    public interface IPlayer
    {
        ICartsHandler CartsHandler { get; }
        void ActivateDefuseState();
        void ActivateAttackState();

        void Deactivate();

        void Remove();
        Transform GetHandTransform();
    }
}