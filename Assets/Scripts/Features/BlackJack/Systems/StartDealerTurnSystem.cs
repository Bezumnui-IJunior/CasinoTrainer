using Features.BlackJack.Components;
using Features.Common.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class StartDealerTurnSystem : ISystem
    {
        private Stash<BetConfirmedTag> _betConfirmedTag;
        private Filter _dealerFilter;
        private Filter _playerFilter;
        private Stash<TurnHolderComponent> _turnHolder;
        public World World { get; set; }

        public void OnAwake()
        {
            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<BetComponent>()
                .Without<BetConfirmedTag>()
                .Build();

            _dealerFilter = World.Filter
                .With<DealerTag>()
                .Without<TurnHolderTag>()
                .Build();

            _betConfirmedTag = World.GetStash<BetConfirmedTag>();
            _turnHolder = World.GetStash<TurnHolderComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _playerFilter)
            foreach (Entity dealer in _dealerFilter)
            foreach (ref TurnHolderComponent turnHolder in _turnHolder)
            {
                _betConfirmedTag.Add(player);
                turnHolder.Value = dealer.Id;
            }
        }

        public void Dispose() { }
    }
}