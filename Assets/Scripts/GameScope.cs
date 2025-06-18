using Windows;
using Windows.Configs;
using Features.BlackJack;
using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.Card;
using Features.Card.Services;
using Features.Common;
using Features.Dealer;
using Features.Dealer.Services;
using Features.EntityViewFactory;
using Features.GameOver;
using Features.View;
using GameStates;
using Infrastructure;
using Progress;
using Scellecs.Morpeh;
using Sounds;
using Sounds.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View;
using View.Services;

public class GameScope : LifetimeScope
{
    [Header("Scriptable Objects")] [SerializeField]
    private PlayerViewConfig _playerViewConfig;

    [SerializeField] private DealerViewConfig _dealerViewConfig;
    [SerializeField] private CardViewConfig _cardViewConfig;
    [SerializeField] private DealerConfig _dealerConfig;
    [SerializeField] private WindowsConfig _windowsConfig;
    [SerializeField] private MusicConfig _musicConfig;
    [SerializeField] private Settings _settings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(World.Default);
        RegisterServices(builder);
        RegisterFactories(builder);
        RegisterFeatures(builder);
        RegisterView(builder);

        builder.RegisterInstance(_playerViewConfig).As<IPlayerViewConfig>();
        builder.RegisterInstance(_dealerViewConfig).As<IDealerViewConfig>();
        builder.RegisterInstance(_cardViewConfig).As<ICardViewConfig>();
        builder.RegisterInstance(_dealerConfig).As<IDealerConfig>();
        builder.RegisterInstance(_windowsConfig).As<IWindowsConfig>();
        builder.RegisterInstance(_musicConfig).As<IMusicConfig>();
        builder.RegisterInstance(_settings).As<ISettings>();

        RegisterInjector(builder);
        RegisterStates(builder);

        builder.RegisterEntryPoint<EntryPoint>();
    }

    private void RegisterInjector(IContainerBuilder builder)
    {
        builder.Register<GlobalInjector>(Lifetime.Singleton);
    }

    private void RegisterStates(IContainerBuilder builder)
    {
        builder.Register<StateMachine>(Lifetime.Singleton).As<IStateMachine>();
        builder.Register<BootstrapState>(Lifetime.Singleton);
        builder.Register<SetupMusicState>(Lifetime.Singleton);
        builder.Register<MainMenuState>(Lifetime.Singleton);
        builder.Register<BlackJackStartState>(Lifetime.Singleton);
        builder.Register<BlackJackRunningState>(Lifetime.Singleton);
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.Register<Instantilizer>(Lifetime.Singleton).As<IInstantilizer>();
        builder.Register<SceneFactory>(Lifetime.Singleton).As<ISceneFactory>();
        builder.Register<WindowsManager>(Lifetime.Singleton).As<IWindowsManager>();
        builder.Register<FeatureFactory>(Lifetime.Singleton).As<IFeatureFactory>();
        builder.Register<CardOwnership>(Lifetime.Singleton).As<ICardOwnership>();
        builder.Register<ScoreCalculator>(Lifetime.Singleton).As<IScoreCalculator>();
        builder.Register<Indexer>(Lifetime.Singleton).As<IIndexer>();
        builder.Register<BackgroundMusic>(Lifetime.Singleton).As<IBackgroundMusic>();
        builder.RegisterInstance(PlayerData.LoadOrDefault()).As<IPlayerData>();
    }

    private void RegisterFeatures(IContainerBuilder builder)
    {
        builder.Register<EntityViewFactoryFeature>(Lifetime.Singleton);
        builder.Register<BlackJackFeature>(Lifetime.Singleton);
        builder.Register<DealerFeature>(Lifetime.Singleton);
        builder.Register<CardFeature>(Lifetime.Singleton);
        builder.Register<CommonFeatures>(Lifetime.Singleton);
        builder.Register<ViewFeature>(Lifetime.Singleton);
        builder.Register<GameOverFeature>(Lifetime.Singleton);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<DealerFactory>(Lifetime.Singleton).As<IDealerFactory>();
        builder.Register<DeckFactory>(Lifetime.Singleton).As<IDeckFactory>();
        builder.Register<GameOverFactory>(Lifetime.Singleton).As<IGameOverFactory>();
        builder.Register<PlayerFactory>(Lifetime.Singleton).As<IPlayerFactory>();
    }

    private void RegisterView(IContainerBuilder builder)
    {
        builder.Register<DealerCollectAnimation>(Lifetime.Singleton).As<IDealerCollectAnimation>();
        builder.Register<PlayerCollectAnimation>(Lifetime.Singleton).As<IPlayerCollectAnimation>();
    }
}