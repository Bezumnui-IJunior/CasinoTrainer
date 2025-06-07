using DG.Tweening;
using Features.BlackJack;
using Features.Card;
using Features.Common;
using Features.Dealer;
using Features.EntityViewFactory;
using Features.View;
using Infrastructure;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;
using VContainer;

public class EcsEntryPoint : MonoBehaviour
{
    private IFeatureFactory _factory;
    private World _world;

    private void Start()
    {
        int order = 0;
        DOTween.Init();
        _world = World.Default;
        
        _world
            .AddFeature(++order, _factory.Create<CommonFeatures>())
            .AddFeature(++order, _factory.Create<EntityViewFactoryFeature>())
            .AddFeature(++order, _factory.Create<DealerFeature>())
            .AddFeature(++order, _factory.Create<CardFeature>())
            .AddFeature(++order, _factory.Create<BlackJackFeature>())
            .AddFeature(++order, _factory.Create<ViewFeature>());
    }

    [Inject]
    private void Initialize(IFeatureFactory factory)
    {
        _factory = factory;
    }
}