using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerRotatingDeciderSystem : ISystem
    {
        private Filter _dealerFilter;
        private Stash<DecidedTag> _decided;
        private Stash<HiddenCardsComponent> _hiddenCards;
        private Stash<RotatingRequestTag> _rotatingRequest;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<PlayedIntroTag>()
                .With<DealerTag>()
                .With<TurnHolderTag>()
                .With<HiddenCardsComponent>()
                .Without<DecidedTag>()
                .Without<RotatingRequestTag>()
                .Without<TakeCardCooldownComponent>()
                .Build();

            _hiddenCards = World.GetStash<HiddenCardsComponent>();
            _rotatingRequest = World.GetStash<RotatingRequestTag>();
            _decided = World.GetStash<DecidedTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
            {
                if (_hiddenCards.Get(dealer).Value > 0)
                {
                    _rotatingRequest.Add(dealer);
                    _decided.Add(dealer);
                }
            }
        }

        public void Dispose() { }
    }
}