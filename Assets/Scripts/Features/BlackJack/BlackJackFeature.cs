using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.BlackJack.Systems;
using Features.Card.Services;
using Features.Common.Systems;
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
        private readonly IDeckFactory _deckFactory;
        private readonly IPlayerCollectAnimation _playerCollectAnimation;
        private readonly IScoreCalculator _scoresCalculator;

        [Inject]
        public BlackJackFeature(IDeckFactory deckFactory, IScoreCalculator scoresCalculator, IDealerFactory dealerFactory, IPlayerCollectAnimation playerCollectAnimation, IDealerConfig dealerConfig)
        {
            _deckFactory = deckFactory;
            _scoresCalculator = scoresCalculator;
            _playerCollectAnimation = playerCollectAnimation;
        }

        protected override void Initialize()
        {
            AddInitializer(new DeckInitializeSystem(_deckFactory));
            AddInitializer(new PlayerInitializeSystem(_playerCollectAnimation));
            
            AddSystem(new DelegateTurnToDealerSystem());
            AddSystem(new RemoveTurnHolderSystem());
            AddSystem(new PlayerShouldTakeCardSystem());
            AddSystem(new FaceUpCardSystem());
            AddSystem(new TakeCardSystem());
            AddSystem(new CalculateCardsCountSystem(_scoresCalculator));
            AddSystem(new CalculateScoreSystem(_scoresCalculator));
            AddSystem(new CalculateHiddenCardsSystem());

            AddSystem(new CardTakenCleanup());
            AddSystem(new CardRotateCleanup());
        }
    }
}