using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Features.Card.Components;
using Features.View.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class RecalculateScoreSystem : ISystem
    {
        private readonly IScoreCalculator _scoreCalculator;
        private Event<CardTakenEvent> _cardTakenEvent;
        private Filter _holderFilter;

        private Stash<OwnerComponent> _owner;
        private Stash<ScoreComponent> _score;

        public RecalculateScoreSystem(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _holderFilter = World.Filter
                .With<CardHolderTag>()
                .With<ScoreComponent>()
                .Build();

            _owner = World.GetStash<OwnerComponent>();
            _score = World.GetStash<ScoreComponent>();
            _cardTakenEvent = World.GetEvent<CardTakenEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (CardTakenEvent cardTakenEvent in _cardTakenEvent.publishedChanges)
            {
                Entity card = cardTakenEvent.Entity;

                if (_owner.Has(card) == false)
                    continue;

                Entity owner = _owner.Get(card).Value;

                if (_holderFilter.Has(owner) == false)
                    continue;

                _score.Get(owner).Value = _scoreCalculator.GetSumOfHolder(owner);
            }
        }

        public void Dispose() { }
    }
}