using System.Collections.Generic;
using Services.Factories;
using UnityEngine;

namespace GameLogic
{
    class PlayersHandler : IPlayersHandler
    {
        public List<IPlayer> Players { get;private set; }
        public IPlayer MainPlayer { get; private set; }
        public void RemovePlayers()
        {
            foreach (var player in Players)
            {
                player.Remove();
            }
            Players.Clear();
        }

        private readonly IGameFactory _gameFactory;

        public PlayersHandler(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void InitPlayers(int botCount)
        {
            if (Players != null && Players.Count > 0)
            {
                Players.ForEach(p => p.Remove());
                Players.Clear();  
            } 
            Players = new List<IPlayer> { _gameFactory.CreatePlayer() };
            MainPlayer = Players[0];
            for (int i = 0; i < botCount; i++)
            {
                Players.Add(_gameFactory.CreateBot());
            }
        }

        public IPlayer GetNextPlayer(IPlayer player = null)
        {
            if (player == null)
            {
                Debug.Log("Default");
                return Players[0];
            }

            int nextIndex = Players.IndexOf(player) == Players.Count -1 ? 0:Players.IndexOf(player)+1 ;
            return Players[nextIndex];
        }
        
    }
}