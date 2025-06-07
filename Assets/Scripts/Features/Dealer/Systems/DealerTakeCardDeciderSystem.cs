using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerTakeCardDeciderSystem : ISystem
    {
        private const int MaxDealerScore = 17;

        private Filter _dealerFilter;
        private Stash<DealerTakeCardRequestTag> _dealerTakeCardRequest;
        private Stash<ScoreComponent> _score;
        private Stash<DecidedTag> _decided;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DealerTag>()
                .With<TurnHolderTag>()
                .With<ScoreComponent>()
                .Without<DecidedTag>()
                .Without<DealerTakeCardRequestTag>()
                .Without<TakeCardCooldownComponent>()
                .Build();

            _dealerTakeCardRequest = World.GetStash<DealerTakeCardRequestTag>();
            _score = World.GetStash<ScoreComponent>();
            _decided = World.GetStash<DecidedTag>();

        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
            {
                ref int score = ref _score.Get(dealer).Value;

                if (score < MaxDealerScore)
                {
                    _dealerTakeCardRequest.Add(dealer);
                    _decided.Add(dealer);
                }
            }
        }

        public void Dispose() { }
    }
}