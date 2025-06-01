using Scellecs.Morpeh;

namespace Features.Card.Services
{
    public interface IDeckFactory
    {
        Entity CreateCard(World world, Denominations denominal, Suits suit, Entity deck, int order);
    }
}