using Features.BlackJack.Components;
using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerTakeCardSystem : ISystem
    {
        private const int HiddenCard = 2;
        private readonly IScoreCalculator _scoreCalculator;
        private readonly IDealerConfig _dealerConfig;
        private Stash<ShouldTakeCardTag> _shouldTakeCardTag;
        private Filter _filter;
        private Request<TakeCardRequest> _takeCardRequest;
        private Stash<TakeCardTimerComponent> _takeCardTimer;

        public DealerTakeCardSystem(IScoreCalculator scoreCalculator, IDealerConfig dealerConfig)
        {
            _scoreCalculator = scoreCalculator;
            _dealerConfig = dealerConfig;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<ShouldTakeCardTag>()
                .With<CardHolderTag>()
                .With<DealerTag>()
                .Build();

            _shouldTakeCardTag = World.GetStash<ShouldTakeCardTag>();
            _takeCardRequest = World.GetRequest<TakeCardRequest>();
            _takeCardTimer = World.GetStash<TakeCardTimerComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity owner in _filter)
            {
                int totalCards = _scoreCalculator.GetTotalCardsCount(owner);

                if (totalCards == 0)
                    _takeCardTimer.Add(owner).Value = _dealerConfig.TakeCardTimeout;

                _takeCardRequest.Publish(new TakeCardRequest(owner, totalCards == HiddenCard - 1));
                _shouldTakeCardTag.Remove(owner);
            }
        }

        public void Dispose() { }
    }
}