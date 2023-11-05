using System;
using ModestTree.Util;
using UIElements;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IUIService
    {
        void ShowMainMenu(Action startGame);
        void ShowGameHud(Action exitGame);
        void ShowLoadingCurtain();
        void HideLoadingCurtain();
        void ShowEndGameWindow(bool activePlayerWon, Action restartGame, Action exitGame);
    }
    
    

   

}