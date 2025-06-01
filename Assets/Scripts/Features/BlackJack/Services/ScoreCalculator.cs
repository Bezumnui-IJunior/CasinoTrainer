using System.Collections.Generic;
using Features.Card.Components;
using Features.Card.Services;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Services
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ScoreCalculator : IScoreCalculator
    {
        private const int MaxScore = 21;
        private const int AceDowngradeScore = 10;

        private readonly Filter _cardsAnyFilter;

        private readonly Stash<DenominalComponent> _denominal;
        private readonly List<Denominations> _denominations = new List<Denominations>(52);

        private readonly Stash<FaceUpTag> _faceUpTag;
        private readonly Stash<OwnerComponent> _owner;

        private readonly Dictionary<Denominations, int> _scores = new Dictionary<Denominations, int>
        {
            [Denominations.Ace] = 11,
            [Denominations.Two] = 2,
            [Denominations.Three] = 3,
            [Denominations.Four] = 4,
            [Denominations.Five] = 5,
            [Denominations.Six] = 6,
            [Denominations.Seven] = 7,
            [Denominations.Eight] = 8,
            [Denominations.Nine] = 9,
            [Denominations.Ten] = 10,
            [Denominations.Jack] = 10,
            [Denominations.Queen] = 10,
            [Denominations.King] = 10,
        };

        private readonly Filter _cardsFaceUpFilter;

        public ScoreCalculator()
        {
            World world = World.Default!;

            _cardsFaceUpFilter = world.Filter
                .With<OwnerComponent>()
                .With<DenominalComponent>()
                .With<FaceUpTag>()
                .Build();
            
            _cardsAnyFilter = world.Filter
                .With<OwnerComponent>()
                .With<DenominalComponent>()
                .Build();

            _denominal = world.GetStash<DenominalComponent>();
            _owner = world.GetStash<OwnerComponent>();
        }

        public int GetSumOfHolder(Entity owner)
        {
            int totalScore = 0;
            int aceCount = 0;

            foreach (Denominations denomination in GetFaceUpDenominations(owner))
            {
                totalScore += _scores[denomination];

                if (denomination == Denominations.Ace)
                    aceCount++;
            }

            while (totalScore > MaxScore && aceCount-- > 0)
                totalScore -= AceDowngradeScore;

            return totalScore;
        }

        public IReadOnlyCollection<Denominations> GetFaceUpDenominations(Entity owner)
        {
            UpdateFaceUpDenominations(owner);

            return _denominations;
        }

        public int GetTotalCardsCount(Entity owner)
        {
            int count = 0;

            foreach (Entity card in _cardsAnyFilter)
            {
                if (_owner.Get(card).Value == owner)
                    ++count;
            }

            return count;
        }

        private void UpdateFaceUpDenominations(Entity owner)
        {
            _denominations.Clear();

            foreach (Entity card in _cardsFaceUpFilter)
            {
                if (_owner.Get(card).Value == owner)
                    _denominations.Add(_denominal.Get(card).Value);
            }
        }
    }
}