using Features.Card.Services;
using Features.Card.Systems;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Features.Card
{
    public class CardFeature : CombinedFeature
    {
        private readonly ICardsView _cardsView;

        [Inject]
        public CardFeature(ICardsView cardsView)
        {
            _cardsView = cardsView;
        }

        protected override void Initialize()
        {
            AddSystem(new CardSetAppearSystem(_cardsView));
        }
    }
}