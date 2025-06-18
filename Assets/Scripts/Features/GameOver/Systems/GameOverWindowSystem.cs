using Windows;
using Features.BlackJack.Components;
using Features.Dealer.Components;
using Features.GameOver.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Features.GameOver.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverWindowSystem : ISystem
    {
        private readonly IWindowsManager _windowsManager;
        private Filter _dealerFilter;
        private Filter _filter;
        private Filter _playerFilter;
        private Stash<WinnerComponent> _winner;
        private Stash<DrownTag> _drownTag;

        [Inject]
        public GameOverWindowSystem(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<GameOverTag>()
                .With<WinnerComponent>()
                .Without<DrownTag>()
                .Build();

            _playerFilter = World.Filter.With<PlayerTag>().Build();
            _dealerFilter = World.Filter.With<DealerTag>().Build();

            _winner = World.GetStash<WinnerComponent>();
            _drownTag = World.GetStash<DrownTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity winnerEntity in _filter)
            {
                _drownTag.Add(winnerEntity);
                ref WinnerComponent winner = ref _winner.Get(winnerEntity);

                foreach (Entity player in _playerFilter)
                    OpenIfWinner(winner, player, WindowsId.PlayerWinWindow);

                foreach (Entity dealer in _dealerFilter)
                    OpenIfWinner(winner, dealer, WindowsId.PlayerLostWindow);
            }
        }

        public void Dispose() { }

        private void OpenIfWinner(WinnerComponent winner, Entity entity, WindowsId id)
        {
            if (winner.Value == entity.Id)
            {
                _windowsManager.Close(WindowsId.BlackJackPlayButtons);
                _windowsManager.Open(id);
            }
        }
    }
}