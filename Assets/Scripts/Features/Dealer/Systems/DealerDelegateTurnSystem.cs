using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerDelegateTurnSystem : ISystem
    {
        private Filter _dealerFilter;
        private Filter _playerFilter;
        private Filter _turnHolderFilter;
        private Stash<TurnHolderComponent> _turnHolder;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DelegateTurnRequest>()
                .With<DealerTag>()
                .With<CardHolderComponent>()
                .With<HiddenCardsComponent>()
                .With<TurnHolderTag>()
                .Build();

            _playerFilter = World.Filter
                .With<PlayerTag>()
                .Build();
            
            _turnHolderFilter = World.Filter
                .With<TurnHolderComponent>()
                .Build();

            _turnHolder = World.GetStash<TurnHolderComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity _ in _dealerFilter)
            foreach (Entity turnHolder in _turnHolderFilter)
            foreach (Entity player in _playerFilter)
                _turnHolder.Get(turnHolder).Value = player.Id;
           
        }

        public void Dispose() { }
    }
}