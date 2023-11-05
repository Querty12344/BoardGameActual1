using System;
using ModestTree.Util;

namespace GameLogic
{
    public interface IGameLoop
    {
        void StartLoop(Action<bool> endGame);
        void StopLoop();
    }
}