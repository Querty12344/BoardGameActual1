using System.Collections.Generic;
using System.Linq;

namespace GameLogic
{
    public interface IPlayersHandler
    {
        public void InitPlayers(int botCount);
        public IPlayer GetNextPlayer(IPlayer player = null);
        List<IPlayer> Players { get; }
        IPlayer MainPlayer { get;  }
        void RemovePlayers();
    }
}