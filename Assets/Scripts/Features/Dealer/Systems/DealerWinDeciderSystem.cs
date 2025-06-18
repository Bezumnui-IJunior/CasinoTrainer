using Features.BlackJack.Components;
using Features.BlackJack.Services;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerWinDeciderSystem : ISystem
    {
        private readonly IGameOverFactory _gameOverFactory;
        private const int MaxGameScore = 21;

        private Filter _dealerFilter;
        private Stash<ScoreComponent> _score;
        private Stash<DecidedTag> _decided;
        private Filter _playerFilter;

        public DealerWinDeciderSystem(IGameOverFactory gameOverFactory)
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

                if (dealerScore <= MaxGameScore && dealerScore > playerScore || playerScore > MaxGameScore)
                {
                    _gameOverFactory.CreateGameOver(dealer);
                    _decided.Add(dealer);
                }
            }
        }

        public void Dispose() { }
    }
}