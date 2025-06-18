using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerCleanupSystem : ICleanupSystem
    {
        private Stash<DealerTakeCardRequestTag> _dealerTakingCard;
        private Stash<DecidedTag> _decided;
        private Filter _decidedTurnFilter;
        private Stash<DelegateTurnRequest> _delegateTurn;
        private Filter _delegateTurnFilter;
        private Filter _rotatingFilter;
        private Stash<RotatingRequestTag> _rotatingTag;
        private Filter _takingFilter;

        public World World { get; set; }

        public void OnAwake()
        {
            _takingFilter = World.Filter
                .With<DealerTakeCardRequestTag>()
                .Build();

            _rotatingFilter = World.Filter
                .With<RotatingRequestTag>()
                .Build();

            _delegateTurnFilter = World.Filter
                .With<DelegateTurnRequest>()
                .Build();

            _decidedTurnFilter = World.Filter
                .With<DecidedTag>()
                .Build();

            _dealerTakingCard = World.GetStash<DealerTakeCardRequestTag>();
            _rotatingTag = World.GetStash<RotatingRequestTag>();
            _delegateTurn = World.GetStash<DelegateTurnRequest>();
            _decided = World.GetStash<DecidedTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _takingFilter)
                _dealerTakingCard.Remove(dealer);

            foreach (Entity rotatingEntity in _rotatingFilter)
                _rotatingTag.Remove(rotatingEntity);

            foreach (Entity dealer in _delegateTurnFilter)
                _delegateTurn.Remove(dealer);

            foreach (Entity dealer in _decidedTurnFilter)
                _decided.Remove(dealer);
        }

        public void Dispose() { }
    }
}