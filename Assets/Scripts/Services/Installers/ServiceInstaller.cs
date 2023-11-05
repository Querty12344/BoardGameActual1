using DefaultNamespace;
using DefaultNamespace.GameStates;
using GameLogic;
using GameStates;
using Services;
using Services.Factories;
using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private SettingsProvider _settings;
    [SerializeField] private Bootstrap _bootstrap;
    public override void InstallBindings()
    {
        BindGameLogic();
        BindStateMachine();
        BindServices();
        BindFactories();
    }
    private void BindGameLogic()
    {
        Container.Bind<IGameLoop>().To<GameLoop>().AsSingle();
        Container.Bind<ICartDragger>().To<CartDragger>().AsSingle();
        Container.Bind<ICartsProvider>().To<CartsProvider>().AsSingle();
        Container.Bind<IGameTable>().To<GameTable>().AsSingle();
        Container.Bind<GameTableLayout>().AsSingle();
        Container.Bind<IPlayersHandler>().To<PlayersHandler>().AsSingle();
    }

    private void BindStateMachine()
    {
        Container.Bind<BootstrapState>().AsSingle();
        Container.Bind<LevelClearingState>().AsSingle();
        Container.Bind<DataLoadingState>().AsSingle();
        Container.Bind<GameEndingState>().AsSingle();
        Container.Bind<GameLoopState>().AsSingle();
        Container.Bind<LoadLevelState>().AsSingle();
        Container.Bind<MainMenuState>().AsSingle();
        Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
    }

    private void BindServices()
    {
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        Container.Bind<ICartSkinProvider>().To<CartSkinProvider>().AsSingle();
        Container.Bind<ICoroutineRunner>().FromInstance(_bootstrap).AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<ISettingsProvider>().FromInstance(_settings).AsSingle();
        Container.Bind<IUIService>().To<UIService>().AsSingle();
    }

    private void BindFactories()
    {
        Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
    }
}