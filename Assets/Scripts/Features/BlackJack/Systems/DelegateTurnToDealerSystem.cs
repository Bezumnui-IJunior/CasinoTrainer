using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DelegateTurnToDealerSystem : ISystem
    {
        private Filter _tagFilter;
        private Filter _dealerFilter;
        private Filter _turnHolderFilter;
        private Stash<DelegateTurnToDealerRequestTag> _delegate;
        private Stash<TurnHolderComponent> _turnHolder;
        private Stash<FinalTurnTag> _finalTurn;

        public World World { get; set; }

        public void OnAwake()
        {
            _tagFilter = World.Filter
                .With<DelegateTurnToDealerRequestTag>()
                .Build();

            _dealerFilter = World.Filter
                .With<DealerTag>()
                .Build();

            _turnHolderFilter = World.Filter
                .With<TurnHolderComponent>()
                .Build();

            _delegate = World.GetStash<DelegateTurnToDealerRequestTag>();
            _turnHolder = World.GetStash<TurnHolderComponent>();
            _finalTurn = World.GetStash<FinalTurnTag>();

        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity _ in _tagFilter)
            {
                foreach (Entity cardHolder in _turnHolderFilter)
                foreach (Entity dealer in _dealerFilter)
                {
                    _turnHolder.Get(cardHolder).Value = dealer.Id;
                    _finalTurn.Add(dealer);
                }
                
            }
            
            _delegate.RemoveAll();
        }

        public void Dispose() { }
    }
}