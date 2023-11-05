using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;

namespace GameLogic
{
    public interface ICartsProvider
    {
        public void InitCarts();
        public void RemoveAllCarts();
        public void FillWithRandomCarts(List<ICartsHandler> cartsHandler,IPlayer first,bool firstIncluded); 
        Cart SetTrumpSuit();
        bool HasNoCarts();
    }
}