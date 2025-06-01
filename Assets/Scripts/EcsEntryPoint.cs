using DG.Tweening;
using Features.BlackJack;
using Features.Card;
using Features.Common;
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
        DOTween.Init();
        _world = World.Default;

        _world
            .AddFeature(0, _factory.Create<EntityViewFactoryFeature>())
            .AddFeature(1, _factory.Create<BlackJackFeature>())
            .AddFeature(2, _factory.Create<CardFeature>())
            .AddFeature(3, _factory.Create<CommonFeatures>())
            .AddFeature(4, _factory.Create<ViewFeature>());
    }

    [Inject]
    private void Initialize(IFeatureFactory factory)
    {
        _factory = factory;
    }
}