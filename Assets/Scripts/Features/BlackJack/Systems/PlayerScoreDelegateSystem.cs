using Features.BlackJack.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerScoreDelegateSystem : ISystem
    {
        private const int GameMaxScore = 21;
        private Stash<DelegateTurnToDealerRequestTag> _delegateTag;
        private Filter _playerFilter;
        private Stash<ScoreComponent> _score;

        public World World { get; set; }

        public void OnAwake()
        {
            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<TurnHolderTag>()
                .With<ScoreComponent>()
                .Build();

            _score = World.GetStash<ScoreComponent>();
            _delegateTag = World.GetStash<DelegateTurnToDealerRequestTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _playerFilter)
            {
                if (_score.Get(player).Value > GameMaxScore)
                    _delegateTag.Add(World.CreateEntity());
            }
        }

        public void Dispose() { }
    }
}