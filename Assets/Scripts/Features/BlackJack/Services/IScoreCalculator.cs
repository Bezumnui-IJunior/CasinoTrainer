using System.Collections.Generic;
using Features.Card.Services;
using Scellecs.Morpeh;

namespace Features.BlackJack.Services
{
    public interface IScoreCalculator
    {
        int GetSumOfHolder(Entity owner);
        IReadOnlyCollection<Denominations> GetFaceUpDenominations(Entity owner);
        int GetTotalCardsCount(Entity owner);
    }
}