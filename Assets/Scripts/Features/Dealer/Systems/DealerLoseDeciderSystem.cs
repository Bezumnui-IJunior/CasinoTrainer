using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerLoseDeciderSystem : ISystem
    {
        private const int MaxDealerScore = 17;
        private const int MaxGameScore = 21;
        private readonly IGameOverFactory _gameOverFactory;

        private Filter _dealerFilter;
        private Stash<DecidedTag> _decided;
        private Filter _playerFilter;
        private Stash<ScoreComponent> _score;

        [Inject]
        public DealerLoseDeciderSystem(IGameOverFactory gameOverFactory)
        {
            _gameOverFactory = gameOverFactory;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DealerTag>()
                .With<TurnHolderTag>()
                .With<ScoreComponent>()
                .With<FinalTurnTag>()
                .Without<DecidedTag>()
                .Without<DealerTakeCardRequestTag>()
                .Without<TakeCardCooldownComponent>()
                .Build();

            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<ScoreComponent>()
                .Build();

            _score = World.GetStash<ScoreComponent>();
            _decided = World.GetStash<DecidedTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity dealer in _dealerFilter)
            foreach (Entity player in _playerFilter)
            {
                ref int dealerScore = ref _score.Get(dealer).Value;
                ref int playerScore = ref _score.Get(player).Value;

                if (dealerScore > MaxGameScore ||
                    (playerScore > dealerScore && dealerScore >= MaxDealerScore && playerScore <= MaxGameScore))
                {
                    _gameOverFactory.CreateGameOver(player);
                    _decided.Add(dealer);
                }
            }
        }

        public void Dispose() { }
    }
}