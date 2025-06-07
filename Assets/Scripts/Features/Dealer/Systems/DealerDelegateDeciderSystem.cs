using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerDelegateDeciderSystem : ISystem
    {

        private Filter _dealerFilter;
        private Stash<DelegateTurnRequest> _delegateRequest;
        private Stash<HiddenCardsComponent> _hiddenCards;
        private Stash<ScoreComponent> _score;
        private Stash<DecidedTag> _decided;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DealerTag>()
                .With<TurnHolderTag>()
                .With<HiddenCardsComponent>()
                .Without<PlayedIntroTag>()
                .Without<DecidedTag>()
                .Build();

            _hiddenCards = World.GetStash<HiddenCardsComponent>();
            _delegateRequest = World.GetStash<DelegateTurnRequest>();
            _decided = World.GetStash<DecidedTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
            {
                if (_hiddenCards.Get(dealer).Value > 0)
                {
                    _delegateRequest.Add(dealer);
                    _decided.Add(dealer);
                }
               
            }
        }

        public void Dispose() { }
    }
}