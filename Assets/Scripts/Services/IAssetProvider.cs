using System;
using GameLogic;
using Services;
using UIElements;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public interface IAssetProvider
    {
        void LoadAssets(Action callBack);
        PlayerHandLayout GetHandLayout(); 
        PlayerView GetDefaultPlayerView();
        Cart GetCart();
        MainMenu GetMainMenu();
        GameHud GetGameHud();
        LoadingCurtain GetLoadingCurtain();
        EndGameWindow GetEndGameWindow();
    }
}