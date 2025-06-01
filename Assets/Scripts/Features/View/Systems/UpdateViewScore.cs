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
        private Event<CardTakenEvent> _cardTakenEvent;
        private Stash<OwnerComponent> _owner;
        private Stash<PlayerTag> _playerTag;
        private Stash<ScoreComponent> _score;

        public UpdateViewScore(IScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _cardTakenEvent = World.GetEvent<CardTakenEvent>();
            _playerTag = World.GetStash<PlayerTag>();
            _score = World.GetStash<ScoreComponent>();
            _owner = World.GetStash<OwnerComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (CardTakenEvent cardTakenEvent in _cardTakenEvent.publishedChanges)
            {
                Entity card = cardTakenEvent.Entity;

                if (_owner.Has(card) == false)
                    continue;

                Entity owner = _owner.Get(card).Value;

                if (_playerTag.Has(owner) == false || _score.Has(owner) == false)
                    continue;

                _scoreView.UpdateScore(_score.Get(owner).Value);
            }
        }

        public void Dispose() { }
    }
}