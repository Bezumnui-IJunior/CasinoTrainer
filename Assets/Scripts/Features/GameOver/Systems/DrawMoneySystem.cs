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
    public class DrawMoneySystem : ISystem
    {
        private readonly IWindowsManager _windowsManager;
        private Stash<BetComponent> _bet;
        private Filter _dealerFilter;
        private Filter _filter;
        private Filter _playerFilter;
        private Stash<PaidTag> _tag;
        private Stash<WinnerComponent> _winner;
        private Stash<MoneyHolderComponent> _moneyHolder;
        private Stash<SaveRequestTag> _saveRequest;

   
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<GameOverTag>()
                .Without<PaidTag>()
                .Build();

            _playerFilter = World.Filter
                .With<PlayerTag>()
                .With<BetComponent>()
                .With<MoneyHolderComponent>()
                .Build();

            _bet = World.GetStash<BetComponent>();
            _tag = World.GetStash<PaidTag>();
            _moneyHolder = World.GetStash<MoneyHolderComponent>();
            _saveRequest = World.GetStash<SaveRequestTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity gameOver in _filter)
            foreach (Entity player in _playerFilter)
            {
                ref int bet = ref _bet.Get(player).Value;
                _tag.Add(gameOver);

                _moneyHolder.Get(player).Value += bet;
                _saveRequest.Add(player);
            }
        }

        public void Dispose() { }
    }
}