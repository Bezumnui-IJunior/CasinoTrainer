using Windows;
using Features.GameOver.Systems;
using Progress;
using Scellecs.Morpeh.Addons.Feature;
using Unity.IL2CPP.CompilerServices;
using VContainer;

namespace Features.GameOver
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverFeature : CombinedFeature
    {
        private readonly IPlayerData _playerData;
        private readonly IWindowsManager _windowsManager;

        [Inject]
        public GameOverFeature(IWindowsManager windowsManager, IPlayerData playerData)
        {
            _windowsManager = windowsManager;
            _playerData = playerData;
        }

        protected override void Initialize()
        {
            AddSystem(new GameOverWindowSystem(_windowsManager));
            AddSystem(new GameOverDrawSystem(_windowsManager));
            AddSystem(new GameOverTurnSystem());
            AddSystem(new WinMoneySystem(_playerData));
            AddSystem(new DrawMoneySystem(_playerData));
        }
    }
}