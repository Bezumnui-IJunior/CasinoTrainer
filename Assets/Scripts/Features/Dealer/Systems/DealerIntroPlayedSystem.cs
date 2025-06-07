using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerIntroPlayedSystem : ISystem
    {

        private Filter _dealerFilter;
        private Stash<HiddenCardsComponent> _hiddenCards;
        private Stash<PlayedIntroTag> _playedIntroTag;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DealerTag>()
                .With<HiddenCardsComponent>()
                .Without<PlayedIntroTag>()
                .Build();

            _playedIntroTag = World.GetStash<PlayedIntroTag>();
            _hiddenCards = World.GetStash<HiddenCardsComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
            {
                if (_hiddenCards.Get(dealer).Value > 0)
                    _playedIntroTag.Add(dealer);
            }
        }

        public void Dispose() { }
    }
}