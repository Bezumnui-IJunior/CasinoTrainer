using Features.BlackJack.Services;
using Features.View.Systems;
using Scellecs.Morpeh.Addons.Feature;
using Unity.IL2CPP.CompilerServices;
using VContainer;
using View;
using View.Services;

namespace Features.View
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ViewFeature : UpdateFeature
    {
        private readonly IScoreCalculator _scoreCalculator;
        private readonly IScoreView _scoreView;
        private readonly ICardRotateAnimation _cardRotateAnimation;
        private readonly ICardViewConfig _cardViewConfig;

        [Inject]
        public ViewFeature(IScoreView scoreView, IScoreCalculator scoreCalculator, ICardRotateAnimation cardRotateAnimation, ICardViewConfig cardViewConfig)
        {
            _scoreView = scoreView;
            _scoreCalculator = scoreCalculator;
            _cardRotateAnimation = cardRotateAnimation;
            _cardViewConfig = cardViewConfig;
        }

        protected override void Initialize()
        {
            AddSystem(new CardCollectAnimationSystem(_scoreCalculator));
            AddSystem(new RotateAnimationSystem(_cardRotateAnimation, _cardViewConfig));
            AddSystem(new UpdateViewScore(_scoreView));
        }
    }
}