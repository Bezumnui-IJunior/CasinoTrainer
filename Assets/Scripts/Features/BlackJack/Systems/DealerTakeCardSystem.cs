using Features.BlackJack.Components;
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
        private Stash<ShouldTakeCardTag> _allowedToTakeCardTag;
        private Filter _filter;
        private Request<TakeCardRequest> _takeCardRequest;

        public DealerTakeCardSystem(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<CardHolderTag>()
                .With<ShouldTakeCardTag>()
                .With<DealerTag>()
                .Build();

            _allowedToTakeCardTag = World.GetStash<ShouldTakeCardTag>();
            _takeCardRequest = World.GetRequest<TakeCardRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity owner in _filter)
            {
                _takeCardRequest.Publish(new TakeCardRequest(owner, _scoreCalculator.GetTotalCardsCount(owner) == HiddenCard - 1));
                _allowedToTakeCardTag.Remove(owner);
            }
        }

        public void Dispose() { }
    }
}