using Features.BlackJack.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RemoveTurnHolderSystem : ISystem
    {
        private Stash<TurnHolderComponent> _turnHolderComponent;
        private Filter _turnHolderComponentFilter;
        private Stash<TurnHolderTag> _turnHolderTag;
        private Filter _turnHolderTagFilter;

        public World World { get; set; }

        public void OnAwake()
        {
            _turnHolderTagFilter = World.Filter
                .With<TurnHolderTag>()
                .Build();

            _turnHolderComponentFilter = World.Filter
                .With<TurnHolderComponent>()
                .Build();

            _turnHolderComponent = World.GetStash<TurnHolderComponent>();
            _turnHolderTag = World.GetStash<TurnHolderTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity holderComponent in _turnHolderComponentFilter)
            foreach (Entity holderTag in _turnHolderTagFilter)
            {
                if (_turnHolderComponent.Get(holderComponent).Value != holderTag.Id)
                    _turnHolderTag.Remove(holderTag);
            }
        }

        public void Dispose() { }

    }

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class AddTurnHolderSystem : ISystem
    {
        private Filter _cardHolderFilter;
        private Stash<TurnHolderComponent> _turnHolderComponent;
        private Filter _turnHolderComponentFilter;
        private Stash<TurnHolderTag> _turnHolderTag;
        private Filter _turnHolderTagFilter;

        public World World { get; set; }

        public void OnAwake()
        {
            _turnHolderTagFilter = World.Filter
                .With<TurnHolderTag>()
                .Build();

            _turnHolderComponentFilter = World.Filter
                .With<TurnHolderComponent>()
                .Build();

            _cardHolderFilter = World.Filter
                .With<CardHolderComponent>()
                .Build();

            _turnHolderComponent = World.GetStash<TurnHolderComponent>();
            _turnHolderTag = World.GetStash<TurnHolderTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity holder in _turnHolderComponentFilter)
            foreach (Entity cardHolder in _cardHolderFilter)
            {
                if (_turnHolderComponent.Get(holder).Value == cardHolder.Id && _turnHolderTag.Has(cardHolder) == false)
                    _turnHolderTag.Add(cardHolder);
            }
        }

        public void Dispose() { }

      
    }
}