using Features.BlackJack.Services;
using Features.BlackJack.Systems;
using Features.View.Systems;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;
using View;
using View.Services;

namespace Features.View
{
    public class ViewFeature : UpdateFeature
    {
        private readonly IScoreView _scoreView;
        private readonly IScoreCalculator _scoreCalculator;
        private readonly IPlayerViewConfig _playerViewConfig;

        [Inject]
        public ViewFeature(IScoreView scoreView, IScoreCalculator scoreCalculator, IPlayerViewConfig playerViewConfig)
        {
            _scoreView = scoreView;
            _scoreCalculator = scoreCalculator;
            _playerViewConfig = playerViewConfig;
        }

        protected override void Initialize()
        {
            AddSystem(new TakeCardSystem());
            AddSystem(new CardCollectAnimationSystem(_scoreCalculator));
            AddSystem(new UpdateViewScore(_scoreView));
            AddSystem(new PlayerDoneCollectingSystem());
        }
    }
}