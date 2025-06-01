using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Features.Card.Components;
using Features.EntityViewFactory.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using View.Services;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CardCollectAnimationSystem : ISystem
    {

        private readonly IScoreCalculator _scoreCalculator;
        private Event<CardTakenEvent> _cardTakenEvent;
        private Filter _cardFilter;
        private Filter _ownerFilter;
        private Stash<OwnerComponent> _owner;
        private Stash<ViewComponent> _view;
        private Stash<CollectAnimationComponent> _collectAnimation;
        private Stash<FaceUpTag> _faceUpTag;

        public CardCollectAnimationSystem(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _cardFilter = World.Filter
                .With<ViewComponent>()
                .With<OwnerComponent>()
                .Build();

            _ownerFilter = World.Filter
                .With<CardHolderTag>()
                .With<CollectAnimationComponent>()
                .Build();

            
            _cardTakenEvent = World.GetEvent<CardTakenEvent>();
            _view = World.GetStash<ViewComponent>();
            _owner = World.GetStash<OwnerComponent>();
            _collectAnimation = World.GetStash<CollectAnimationComponent>();
            _faceUpTag = World.GetStash<FaceUpTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (CardTakenEvent cardTakenEvent in _cardTakenEvent.publishedChanges)
            {
                Entity card = cardTakenEvent.Entity;

                if (_cardFilter.Has(card) == false)
                    continue;

                ref OwnerComponent owner = ref _owner.Get(card);

                if (_ownerFilter.Has(owner.Value) == false)
                    continue;

                Transform transform = _view.Get(card).Value.transform;

                int cardsCount = _scoreCalculator.GetTotalCardsCount(owner.Value);
               
                _collectAnimation.Get(owner.Value).Value.OnCollect(transform, cardsCount, _faceUpTag.Has(card));
            }
        }

        public void Dispose() { }
    }
}