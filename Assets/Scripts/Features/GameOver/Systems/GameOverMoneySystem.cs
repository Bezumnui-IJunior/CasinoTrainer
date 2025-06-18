using Windows;
using Features.BlackJack.Components;
using Features.GameOver.Components;
using Progress;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.GameOver.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverMoneySystem : ISystem
    {
        private readonly IPlayerData _playerData;
        private readonly IWindowsManager _windowsManager;
        private Filter _dealerFilter;
        private Filter _filter;
        private Filter _playerFilter;
        private Stash<WinnerComponent> _winner;
        private Stash<BetComponent> _bet;
        private Stash<PaidTag> _tag;
        public World World { get; set; }

        public GameOverMoneySystem(IPlayerData playerData)
        {
            _playerData = playerData;
        }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<GameOverTag>()
                .With<WinnerComponent>()
                .Without<PaidTag>()
                .Build();

            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<BetComponent>()
                .Build();

            _winner = World.GetStash<WinnerComponent>();
            _bet = World.GetStash<BetComponent>();
            _tag = World.GetStash<PaidTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity winnerEntity in _filter)
            foreach (Entity player in _playerFilter)
            {
                _tag.Add(winnerEntity);
                ref int bet = ref _bet.Get(player).Value;

                Debug.Log($"Initial score: {_playerData.PlayerMoney}");

                if (_winner.Get(winnerEntity).Value == player.Id)
                    _playerData.PlayerMoney += bet;
                else
                    _playerData.PlayerMoney -= bet;

                Debug.Log($"Current score: {_playerData.PlayerMoney}");

                _playerData.Save();
            }
        }

        public void Dispose() { }
    }
}