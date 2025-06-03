using Features.BlackJack.Components;
using Features.Card.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TakeCardSystem : ISystem
    {
        private Filter _cardsFilter;
        private Event<CardTakenEvent> _cardTakenEvent;
        private Filter _deckFilter;
        private Stash<FaceUpTag> _faceUpTag;
        private Stash<OrderComponent> _order;

        private Stash<OwnerComponent> _owner;

        private Filter _playerTag;
        private Request<TakeCardRequest> _request;

        public World World { get; set; }

        public void Dispose() { }

        public void OnAwake()
        {
            _playerTag = World.Filter
                .With<CardHolderTag>()
                .With<PlayerTag>()
                .Build();

            _deckFilter = World.Filter
                .With<DeckTag>()
                .With<CardHolderTag>()
                .Build();

            _cardsFilter = World.Filter
                .With<OwnerComponent>()
                .With<DenominalComponent>()
                .With<SuitComponent>()
                .With<OrderComponent>()
                .Build();

            _owner = World.GetStash<OwnerComponent>();
            _order = World.GetStash<OrderComponent>();
            _request = World.GetRequest<TakeCardRequest>();
            _cardTakenEvent = World.GetEvent<CardTakenEvent>();
            _faceUpTag = World.GetStash<FaceUpTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (TakeCardRequest request in _request.Consume())
            {
                if (request.HasRequestee)
                    GiveCardToNewOwner(request.Requestee, request.HideCard);
                else
                    GiveCardToPlayer();
            }
        }

        private void GiveCardToPlayer()
        {
            foreach (Entity player in _playerTag)
                GiveCardToNewOwner(player, false);
        }

        private void GiveCardToNewOwner(Entity newOwner, bool shouldHideCard)
        {
            foreach (Entity deck in _deckFilter)
            {
                foreach (Entity card in _cardsFilter)
                {
                    ref OwnerComponent owner = ref _owner.Get(card);
                    ref OrderComponent order = ref _order.Get(card);

                    if (owner.Value.Id != deck.Id)
                        continue;

                    if (order.Value-- != 0)
                        continue;

                    if (shouldHideCard == false)
                        _faceUpTag.Add(card);

                    owner.Value = newOwner;
                    _cardTakenEvent.NextFrame(new CardTakenEvent(card));
                }
            }
        }
    }
}