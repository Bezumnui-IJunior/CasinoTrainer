using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.BlackJack.Systems;
using Features.Card.Services;
using Scellecs.Morpeh.Addons.Feature;
using Unity.IL2CPP.CompilerServices;
using VContainer;
using View.Services;

namespace Features.BlackJack
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class BlackJackFeature : CombinedFeature
    {
        private readonly IDealerFactory _dealerFactory;
        private readonly IDeckFactory _deckFactory;
        private readonly IPlayerCollectAnimation _playerCollectAnimation;
        private readonly IScoreCalculator _scoresCalculator;
        private readonly IDealerConfig _dealerConfig;

        [Inject]
        public BlackJackFeature(IDeckFactory deckFactory, IScoreCalculator scoresCalculator, IDealerFactory dealerFactory, IPlayerCollectAnimation playerCollectAnimation, IDealerConfig dealerConfig)
        {
            _deckFactory = deckFactory;
            _scoresCalculator = scoresCalculator;
            _dealerFactory = dealerFactory;
            _playerCollectAnimation = playerCollectAnimation;
            _dealerConfig = dealerConfig;
        }

        protected override void Initialize()
        {
            AddInitializer(new DeckInitializeSystem(_deckFactory));
            AddInitializer(new PlayerInitializeSystem(_playerCollectAnimation));
            AddInitializer(new DealerInitializeSystem(_dealerFactory, _dealerConfig));
            AddSystem(new DealerShouldTakeCardTimerSystem());
            AddSystem(new DealerTakeCardSystem(_scoresCalculator, _dealerConfig));
            AddSystem(new RecalculateScoreSystem(_scoresCalculator));
        }
    }
}