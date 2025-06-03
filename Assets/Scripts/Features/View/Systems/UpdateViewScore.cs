using Features.BlackJack.Components;
using Features.Card.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using View.Services;

namespace Features.View.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class UpdateViewScore : ISystem
    {
        private readonly IScoreView _scoreView;
        private Stash<OwnerComponent> _owner;
        private Stash<PlayerTag> _playerTag;
        private Stash<ScoreComponent> _score;
        private Filter _filter;

        public UpdateViewScore(IScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<TakenTag>()
                .With<OwnerComponent>()
                .Build();
            
            _playerTag = World.GetStash<PlayerTag>();
            _score = World.GetStash<ScoreComponent>();
            _owner = World.GetStash<OwnerComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity taken in _filter)
            {
                Entity owner = _owner.Get(taken).Value;

                if (_playerTag.Has(owner) == false || _score.Has(owner) == false)
                    continue;

                _scoreView.UpdateScore(_score.Get(owner).Value);
            }
        }

        public void Dispose() { }
    }
}