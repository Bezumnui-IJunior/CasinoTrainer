using Scellecs.Morpeh;

namespace Features.BlackJack.Services
{
    public interface ICardOwnership
    {
        bool IsOwnedBy(Entity card, Entity owner);
    }
}