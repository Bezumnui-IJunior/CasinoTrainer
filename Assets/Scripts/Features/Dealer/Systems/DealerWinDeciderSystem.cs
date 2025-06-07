using Features.BlackJack.Components;
using Features.Dealer.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Dealer.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DealerWinDeciderSystem : ISystem
    {
        private const int MaxGameScore = 21;


        private Filter _dealerFilter;
        private Stash<ScoreComponent> _score;
        private Stash<DecidedTag> _decided;
        private Filter _playerFilter;
        private Stash<WinTag> _win;

        public World World { get; set; }

        public void OnAwake()
        {
            _dealerFilter = World.Filter
                .With<DealerTag>()
                .With<TurnHolderTag>()
                .With<ScoreComponent>()
                .With<FinalTurnTag>()
                .Without<LoseTag>()
                .Without<WinTag>()
                .Without<DecidedTag>()
                .Without<DealerTakeCardRequestTag>()
                .Without<TakeCardCooldownComponent>()
                .Build();
            
            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<ScoreComponent>()
                .Build();

            _win = World.GetStash<WinTag>();
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

                if (dealerScore <= MaxGameScore && dealerScore >= playerScore || playerScore > MaxGameScore)
                {
                    Debug.Log("Dealer win");

                    _win.Add(dealer);
                    _decided.Add(dealer);
                }
            }
        }

        public void Dispose() { }
    }
}