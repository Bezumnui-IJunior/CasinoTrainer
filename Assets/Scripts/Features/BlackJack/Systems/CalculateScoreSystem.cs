using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Features.Card.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CalculateScoreSystem : ISystem
    {
        private readonly IScoreCalculator _scoreCalculator;
        private Filter _cardFilter;
        private Filter _cardHolderFilter;
        private Stash<OwnerComponent> _owner;
        private Stash<ScoreComponent> _score;

        public CalculateScoreSystem(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _cardFilter = World.Filter
                .With<OwnerComponent>()
                .Build();

            _cardHolderFilter = World.Filter
                .With<ScoreComponent>()
                .With<CardHolderComponent>()
                .Build();

            _owner = World.GetStash<OwnerComponent>();
            _score = World.GetStash<ScoreComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity cardHolder in _cardHolderFilter)
            foreach (Entity taken in _cardFilter)
            {
                if (_owner.Get(taken).Value.Id != cardHolder.Id)
                    continue;

                _score.Get(cardHolder).Value = _scoreCalculator.GetSumOfHolder(cardHolder);
            }
        }

        public void Dispose() { }
    }
}