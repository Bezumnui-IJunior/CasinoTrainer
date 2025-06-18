using Windows;
using Features.BlackJack.Components;
using Features.GameOver.Components;
using Progress;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.GameOver.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class WinMoneySystem : ISystem
    {
        private const int WinMultiplier = 2;
        private readonly IPlayerData _playerData;
        private readonly IWindowsManager _windowsManager;
        private Stash<BetComponent> _bet;
        private Filter _dealerFilter;
        private Filter _filter;
        private Filter _playerFilter;
        private Stash<PaidTag> _tag;
        private Stash<WinnerComponent> _winner;

        public WinMoneySystem(IPlayerData playerData)
        {
            _playerData = playerData;
        }

        public World World { get; set; }

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

                if (_winner.Get(winnerEntity).Value == player.Id)
                    _playerData.AddMoney(bet * WinMultiplier);

                _playerData.Save();
            }
        }

        public void Dispose() { }
    }
}