using Features.BlackJack.Components;
using Features.BlackJack.Configs;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerCooldownScheduleSystem : ISystem
    {
        private readonly IDealerConfig _dealerConfig;
        private Filter _dealerFilter;
        private Stash<TakeCardCooldownComponent> _takeCardTimer;

        public DealerCooldownScheduleSystem(IDealerConfig dealerConfig)
        {
            _dealerConfig = dealerConfig;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DealerTag>()
                .With<CardHolderComponent>()
                .With<TurnHolderTag>()
                .Without<TakeCardCooldownComponent>()
                .Build();

            _takeCardTimer = World.GetStash<TakeCardCooldownComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
                _takeCardTimer.Add(dealer).Value = _dealerConfig.TakeCardTimeout;
        }

        public void Dispose() { }
    }
}