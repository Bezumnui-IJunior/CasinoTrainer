using Windows;
using Features.BlackJack.Configs;
using Features.BlackJack.Services;
using Features.BlackJack.Systems;
using Features.Card.Services;
using Features.Common.Systems;
using Features.Dealer.Services;
using Features.GameOver.Systems;
using Infrastructure;
using Progress;
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
        private readonly IScoreCalculator _scoresCalculator;
        private readonly IStateMachine _stateMachine;
        private readonly IWindowsManager _windowsManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly IPlayerData _playerData;

        [Inject]
        public BlackJackFeature(IDeckFactory deckFactory, IScoreCalculator scoresCalculator, IDealerFactory dealerFactory, IDealerConfig dealerConfig,
            IStateMachine stateMachine, IWindowsManager windowsManager, IPlayerFactory playerFactory, IPlayerData playerData)
        {
            _deckFactory = deckFactory;
            _scoresCalculator = scoresCalculator;
            _stateMachine = stateMachine;
            _windowsManager = windowsManager;
            _playerFactory = playerFactory;
            _playerData = playerData;
        }

        protected override void Initialize()
        {
            AddInitializer(new DeckInitializeSystem(_deckFactory));
            AddInitializer(new PlayerInitializeSystem(_playerFactory));
            
            AddSystem(new StartDealerTurnSystem());
            AddSystem(new DelegateTurnToDealerSystem());
            AddSystem(new RemoveTurnHolderSystem());
            AddSystem(new PlayerShouldTakeCardSystem());
            // AddSystem(new PlayerScoreDelegateSystem());
            AddSystem(new FaceUpCardSystem());
            AddSystem(new TakeCardSystem());
            AddSystem(new CalculateCardsCountSystem(_scoresCalculator));
            AddSystem(new CalculateScoreSystem(_scoresCalculator));
            AddSystem(new CalculateHiddenCardsSystem());

            AddSystem(new CardTakenCleanup());
            AddSystem(new CardRotateCleanup());
            AddSystem(new RestartGameOnRequestSystem(_stateMachine));
        }
    }
}