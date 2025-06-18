using Features.BlackJack.Services;
using Features.Card.Systems;
using Features.View.Systems;
using Scellecs.Morpeh.Addons.Feature;
using Unity.IL2CPP.CompilerServices;
using VContainer;
using View;

namespace Features.View
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ViewFeature : UpdateFeature
    {
        private readonly ICardViewConfig _cardViewConfig;
        private readonly IScoreCalculator _scoreCalculator;

        [Inject]
        public ViewFeature(IScoreCalculator scoreCalculator, ICardViewConfig cardViewConfig)
        {
            _scoreCalculator = scoreCalculator;
            _cardViewConfig = cardViewConfig;
        }

        protected override void Initialize()
        {
            AddInitializer(new CardsViewInitSystem());
            AddInitializer(new RotateAnimationInitSystem());

            AddSystem(new CardsViewSystem());
            AddSystem(new RotateAnimationSetupSystem());
            AddSystem(new CardSetAppearSystem());
            AddSystem(new CardCollectAnimationSystem(_scoreCalculator));

            AddSystem(new RotateAnimationSystem(_cardViewConfig));
        }
    }
}