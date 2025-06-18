using Features.BlackJack.Components;
using Features.Card.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TakeCardSystem : ISystem
    {
        private Stash<CardHolderComponent> _cardHolder;
        private Filter _cardHolderFilter;
        private Filter _cardsFilter;
        private Stash<TakenTag> _cardTakenTag;
        private Filter _deckFilter;
        private Stash<FaceUpTag> _faceUpTag;
        private Stash<OrderComponent> _order;
        private Stash<OwnerComponent> _owner;
        private Stash<TakeCardRequestTag> _shouldTakeCardTag;

        public World World { get; set; }

        public void Dispose() { }

        public void OnAwake()
        {
            _cardHolderFilter = World.Filter
                .With<TakeCardRequestTag>()
                .With<CardHolderComponent>()
                .Build();

            _deckFilter = World.Filter
                .With<DeckTag>()
                .With<CardHolderComponent>()
                .Build();

            _cardsFilter = World.Filter
                .With<OwnerComponent>()
                .With<DenominalComponent>()
                .With<SuitComponent>()
                .With<OrderComponent>()
                .Build();

            _owner = World.GetStash<OwnerComponent>();
            _order = World.GetStash<OrderComponent>();
            _shouldTakeCardTag = World.GetStash<TakeCardRequestTag>();
            _cardTakenTag = World.GetStash<TakenTag>();
            _faceUpTag = World.GetStash<FaceUpTag>();
            _cardHolder = World.GetStash<CardHolderComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity taker in _cardHolderFilter)
                GiveCardToNewOwner(taker, _shouldTakeCardTag.Get(taker).ShouldHide);
        }

        private void GiveCardToNewOwner(Entity newOwner, bool shouldHideCard)
        {
            foreach (Entity deck in _deckFilter)
            foreach (Entity card in _cardsFilter)
            {
                ref OwnerComponent owner = ref _owner.Get(card);
                ref OrderComponent order = ref _order.Get(card);

                if (owner.Value.Id != deck.Id)
                    continue;

                if (order.Value-- != 0)
                    continue;

                int totalCards = _cardHolder.Get(owner.Value).Value;
                owner.Value = newOwner;

                if (shouldHideCard == false)
                    _faceUpTag.Add(card);
                order.Value = totalCards;
                _cardTakenTag.Add(card);
            }
        }
    }
}