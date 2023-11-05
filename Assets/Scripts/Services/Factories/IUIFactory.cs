using System;
using DefaultNamespace;
using UIElements;
using Unity.VisualScripting;

namespace Services.Factories
{
    public interface IUIFactory
    {
        MainMenu CreateMainMenu(Action startGame);
        GameHud CreateGameHud(Action exitGame);
        LoadingCurtain CreateLoadingCurtain();
        EndGameWindow CreateEndGameWindow(bool playerWon,Action restartGame,Action exitGame);
    }
}