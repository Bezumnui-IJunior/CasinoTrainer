using System;
using System.Collections.Generic;
using Features.BlackJack;
using Features.Card;
using Features.Common;
using Features.Dealer;
using Features.EntityViewFactory;
using Features.GameOver;
using Features.View;
using Infrastructure;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature;

namespace GameStates
{
    public sealed class BlackJackFeatures
    {
        private readonly IFeatureFactory _factory;
        private readonly IIndexer _indexer;
        private readonly List<BaseFeature> _features = new List<BaseFeature>();
        private readonly List<Type> _featuresTypes;
        private readonly World _world;

        public BlackJackFeatures(World world, IFeatureFactory factory, IIndexer indexer)
        {
            _world = world;
            _factory = factory;
            _indexer = indexer;

            _featuresTypes = new List<Type>
            {
                typeof(CommonFeatures),
                typeof(EntityViewFactoryFeature),
                typeof(DealerFeature),
                typeof(CardFeature),
                typeof(GameOverFeature),
                typeof(BlackJackFeature),
                typeof(ViewFeature),
            };
        }

        public void AddFeatures()
        {
            foreach (Type featureType in _featuresTypes)
            {
                BaseFeature feature = _factory.Create(featureType);
                _features.Add(feature);
                _world.AddFeature(_indexer.Next(), feature);
            }
        }

        public void RemoveFeatures()
        {
            foreach (BaseFeature feature in _features)
                _world.RemoveFeature(feature);

            _features.Clear();
        }
    }
}