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
        private const int GameMaxScore = 21;
        private Filter _playerFilter;
        private Stash<TakeCardRequestTag> _shouldTakeCard;
        private Filter _tagFilter;
        private Stash<ScoreComponent> _score;

        public World World { get; set; }

        public void OnAwake()
        {
            _tagFilter = World.Filter
                .With<PlayerConsumeRequestTag>()
                .Build();

            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<TurnHolderTag>()
                .With<ScoreComponent>()
                .Without<TakeCardRequestTag>()
                .Build();

            _shouldTakeCard = World.GetStash<TakeCardRequestTag>();
            _score = World.GetStash<ScoreComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity _ in _tagFilter)
            foreach (Entity player in _playerFilter)
            {
                if (_score.Get(player).Value <= GameMaxScore)
                    _shouldTakeCard.Add(player);
            }
        }

        public void Dispose() { }
    }
}