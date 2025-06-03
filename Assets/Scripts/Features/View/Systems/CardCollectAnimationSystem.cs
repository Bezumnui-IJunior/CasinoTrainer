using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Features.Card.Components;
using Features.EntityViewFactory.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CardCollectAnimationSystem : ISystem
    {
        private readonly IScoreCalculator _scoreCalculator;
        private Filter _cardFilter;
        private Stash<CollectAnimationComponent> _collectAnimation;
        private Stash<FaceUpTag> _faceUpTag;
        private Stash<OwnerComponent> _owner;
        private Filter _ownerFilter;
        private Stash<ViewComponent> _view;

        public CardCollectAnimationSystem(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _cardFilter = World.Filter
                .With<TakenTag>()
                .With<ViewComponent>()
                .With<OwnerComponent>()
                .Build();

            _view = World.GetStash<ViewComponent>();
            _owner = World.GetStash<OwnerComponent>();
            _collectAnimation = World.GetStash<CollectAnimationComponent>();
            _faceUpTag = World.GetStash<FaceUpTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity taken in _cardFilter)
            {
                ref OwnerComponent owner = ref _owner.Get(taken);

                if (_collectAnimation.Has(owner.Value) == false)
                    continue;

                Transform transform = _view.Get(taken).Value.transform;

                int cardsCount = _scoreCalculator.GetTotalCardsCount(owner.Value);

                _collectAnimation.Get(owner.Value).Value.OnCollect(transform, cardsCount, _faceUpTag.Has(taken));
            }
        }

        public void Dispose() { }
    }
}