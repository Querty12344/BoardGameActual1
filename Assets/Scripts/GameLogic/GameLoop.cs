using System;
using System.Collections;
using System.Linq;
using GameLogic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameLoop:IGameLoop
    {
        private readonly IPlayersHandler _players;
        private IGameTable _gameTable;
        private readonly ICartsProvider _cartsProvider;
        private IPlayer _defusingPlayer;
        private IPlayer _attackingPlayer;
        private int _attackCircleIndex;
        private Action<bool> _endGame;

        public GameLoop( ICartsProvider cartsProvider, IPlayersHandler players, IGameTable gameTable)
        {
            _cartsProvider = cartsProvider;
            _players = players;
            _gameTable = gameTable;
        }


        public void StartLoop(Action<bool> endGame)
        {
            StopLoop();
            _endGame = endGame;
            Cart trumpCart = _cartsProvider.SetTrumpSuit();
            _gameTable.SetTrumpSuit(trumpCart);
            _defusingPlayer = _players.GetNextPlayer();
            _cartsProvider.FillWithRandomCarts(_players.Players.Select(p => p.CartsHandler).ToList(),_defusingPlayer,true);
            MoveToNextDefender(true);
        }

        public void StopLoop()
        {
            _defusingPlayer = null;
            _attackingPlayer = null;
            _gameTable.ClearTable();
            _attackCircleIndex = 0;
        }

        private void MoveToNextDefender(bool lastPlayerDefended)
        {
            TryFindPlayerWithoutCarts(out var playerWithNoCarts);
            DeactivateAll();
            _attackCircleIndex = 0;
            if (lastPlayerDefended)
            {
                _gameTable.ClearTable();
                _attackingPlayer = _defusingPlayer;
                _cartsProvider.FillWithRandomCarts(_players.Players.Select(p => p.CartsHandler).ToList(),_defusingPlayer,true);
            }
            else
            {
                _defusingPlayer.CartsHandler.AddCarts(_gameTable.ClearTable().ToList(),_defusingPlayer,_defusingPlayer == _players.MainPlayer);
                _cartsProvider.FillWithRandomCarts(_players.Players.Select(p => p.CartsHandler).ToList(),
                    _defusingPlayer, false);
                _attackingPlayer = _players.GetNextPlayer(_defusingPlayer);
            }
            _defusingPlayer = _players.GetNextPlayer(_attackingPlayer);
            _gameTable.SetState(_attackingPlayer,true,ActivateCurrentDefender,null);
            Debug.Log(_players.Players.IndexOf(_attackingPlayer));
            _attackingPlayer.ActivateAttackState();
        }
        
        private void ActivateNextAttacker()
        {
            var previousAttackingPlayer = _attackingPlayer;
            Debug.Log("ActivateNextAttacker");
            DeactivateAll();
            var nextAttackingPlayer = _players.GetNextPlayer(_attackingPlayer);
            if (nextAttackingPlayer == _defusingPlayer)
            {
                _attackCircleIndex++;
                if (_attackCircleIndex > 1)
                {
                    MoveToNextDefender(true);
                    return;
                }
                nextAttackingPlayer = _players.GetNextPlayer(nextAttackingPlayer);
                if (nextAttackingPlayer == previousAttackingPlayer)
                {
                    MoveToNextDefender(true);
                    return;
                }
            }

            _attackingPlayer = nextAttackingPlayer;
            _gameTable.SetState(_attackingPlayer,true,ActivateCurrentDefender,ActivateNextAttacker);
            _attackingPlayer.ActivateAttackState();
        }

        private void ActivateCurrentDefender()
        {
            if (TryFindPlayerWithoutCarts(out var playerWithNoCarts))
            {
                if (playerWithNoCarts == _defusingPlayer)
                {
                    MoveToNextDefender(true);
                    return;
                } 
            }
            DeactivateAll();
            _gameTable.SetState(_defusingPlayer,false,ActivateCurrentAttacker,DefenderPassed);
            _defusingPlayer.ActivateDefuseState();
        }

        private void ActivateCurrentAttacker()
        {
            if (TryFindPlayerWithoutCarts(out var playerWithNoCarts))
            {
                if (playerWithNoCarts == _defusingPlayer)
                {
                    MoveToNextDefender(true);
                    return;
                }
                if (playerWithNoCarts == _attackingPlayer)
                {
                    ActivateNextAttacker();
                } 
            }
            else
            {
                DeactivateAll();
                _gameTable.SetState(_attackingPlayer,true,ActivateCurrentDefender,ActivateNextAttacker);
                _attackingPlayer.ActivateAttackState();   
            }
        }

        private void DeactivateAll()
        {
            _attackingPlayer?.Deactivate();
            _defusingPlayer?.Deactivate();
        }

        private void DefenderPassed()
        {
            MoveToNextDefender(false);
        }
        
        private bool TryFindPlayerWithoutCarts(out IPlayer playerWithNoCarts)
        {
            playerWithNoCarts = null;
            foreach (var player in _players.Players)
            {
                if (player.CartsHandler.Carts.Count == 0)
                {
                    playerWithNoCarts = player;
                    if (_cartsProvider.HasNoCarts())
                    {
                        _endGame.Invoke(player == _players.MainPlayer);
                    }
                    return true;
                }
            }
            return false;
        }
    }
}