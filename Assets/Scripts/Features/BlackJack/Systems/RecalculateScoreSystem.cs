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
        private Stash<CardHolderTag> _cardHolderTag;
        private Stash<OwnerComponent> _owner;
        private Stash<ScoreComponent> _score;
        private Filter _filter;

        public RecalculateScoreSystem(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<TakenTag>()
                .With<OwnerComponent>()
                .Build();

            _owner = World.GetStash<OwnerComponent>();
            _score = World.GetStash<ScoreComponent>();
            _cardHolderTag = World.GetStash<CardHolderTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity taken in _filter)
            {
                Entity owner = _owner.Get(taken).Value;

                if (_score.Has(owner) == false || _cardHolderTag.Has(owner) == false)
                    continue;

                _score.Get(owner).Value = _scoreCalculator.GetSumOfHolder(owner);
            }
        }

        public void Dispose() { }
    }
}