using Features.BlackJack.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerShouldTakeCardSystem : ISystem
    {
        private Filter _playerFilter;
        private Stash<TakeCardRequestTag> _shouldTakeCard;
        private Filter _tagFilter;

        public World World { get; set; }

        public void OnAwake()
        {
            _tagFilter = World.Filter
                .With<PlayerConsumeRequestTag>()
                .Build();

            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<TurnHolderTag>()
                .Without<TakeCardRequestTag>()
                .Build();

            _shouldTakeCard = World.GetStash<TakeCardRequestTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity _ in _tagFilter)
            foreach (Entity player in _playerFilter)
                _shouldTakeCard.Add(player);
        }

        public void Dispose() { }
    }
}