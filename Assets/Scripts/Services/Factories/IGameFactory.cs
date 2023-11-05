using System.Collections.Generic;
using GameLogic;

namespace Services.Factories
{
    public interface IGameFactory
    {
        public List<Cart> CreateCarts();
        IPlayer CreatePlayer();
        IPlayer CreateBot();
        void Init(PositionsHandler positions);
    }
}