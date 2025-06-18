using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CalculateCardsCountSystem : ISystem
    {
        private readonly IScoreCalculator _scoreCalculator;
        private Stash<CardHolderComponent> _cardHolder;
        private Filter _cardHolderFilter;

        public CalculateCardsCountSystem(IScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public World World { get; set; }

        public void Dispose() { }

        public void OnAwake()
        {
            _cardHolderFilter = World.Filter
                .With<CardHolderComponent>()
                .Build();

            _cardHolder = World.GetStash<CardHolderComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity cardHolder in _cardHolderFilter)
                _cardHolder.Get(cardHolder).Value = _scoreCalculator.GetTotalCardsCount(cardHolder);
        }
    }
}