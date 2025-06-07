using Features.BlackJack;
using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.Card;
using Features.Card.Services;
using Features.Common;
using Features.Dealer;
using Features.Dealer.Services;
using Features.EntityViewFactory;
using Features.View;
using Infrastructure;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View;
using View.Services;

public class GameScope : LifetimeScope
{
    [SerializeField] private DeckFactory _deckFactory;

    [Header("Views")] [SerializeField] private CardsView _cardsView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private CardRotateAnimation _cardRotateAnimation;

    [Header("Scriptable Objects")] [SerializeField]
    private PlayerViewConfig _playerViewConfig;
    [SerializeField] private DealerViewConfig _dealerViewConfig;
    [SerializeField] private CardViewConfig _cardViewConfig;
    [SerializeField] private DealerConfig _dealerConfig;

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
        builder.RegisterInstance(_deckFactory).As<IDeckFactory>();
    }

    private void RegisterServices(IContainerBuilder builder)
    {
        builder.Register<FeatureFactory>(Lifetime.Singleton).As<IFeatureFactory>();
        builder.Register<CardOwnership>(Lifetime.Singleton).As<ICardOwnership>();
        builder.Register<ScoreCalculator>(Lifetime.Singleton).As<IScoreCalculator>();
    }

    private void RegisterFeatures(IContainerBuilder builder)
    {
        builder.Register<EntityViewFactoryFeature>(Lifetime.Singleton);
        builder.Register<BlackJackFeature>(Lifetime.Singleton);
        builder.Register<DealerFeature>(Lifetime.Singleton);
        builder.Register<CardFeature>(Lifetime.Singleton);
        builder.Register<CommonFeatures>(Lifetime.Singleton);
        builder.Register<ViewFeature>(Lifetime.Singleton);
    }

    private void RegisterFactories(IContainerBuilder builder)
    {
        builder.Register<DealerFactory>(Lifetime.Singleton).As<IDealerFactory>();
    }

    private void RegisterView(IContainerBuilder builder)
    {
        builder.RegisterInstance(_cardsView).As<ICardsView>();
        builder.RegisterInstance(_scoreView).As<IScoreView>();
        builder.RegisterInstance(_cardRotateAnimation).As<ICardRotateAnimation>();
        builder.Register<DealerCollectAnimation>(Lifetime.Singleton).As<IDealerCollectAnimation>();
        builder.Register<PlayerCollectAnimation>(Lifetime.Singleton).As<IPlayerCollectAnimation>();
    }
}