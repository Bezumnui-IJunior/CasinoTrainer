using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerTakeCardSystem : ISystem
    {
        private const int HiddenCard = 1;
        
        private Filter _dealerFilter;
        private Stash<CardHolderComponent> _cardHolderTag;
        private Stash<TakeCardRequestTag> _takeCardRequest;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DealerTakeCardRequestTag>()
                .With<DealerTag>()
                .With<TurnHolderTag>()
                .With<CardHolderComponent>()
                .Without<TakeCardRequestTag>()
                .Build();

            _takeCardRequest = World.GetStash<TakeCardRequestTag>();
            _cardHolderTag = World.GetStash<CardHolderComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
            {
                int totalCards = _cardHolderTag.Get(dealer).Value;

                if (totalCards == HiddenCard)
                    _takeCardRequest.Add(dealer).ShouldHide = true;
                else
                    _takeCardRequest.Add(dealer).ShouldHide = false;
            }
        }

        public void Dispose() { }
    }
}