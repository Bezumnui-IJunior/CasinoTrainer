using Windows;
using Features.Player.Systems;
using Progress;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Features.Player
{
    public class PlayerFeature : CombinedFeature
    {
        private readonly IPlayerData _playerData;
        private readonly IWindowsManager _windowsManager;

        [Inject]
        public PlayerFeature(IWindowsManager windowsManager, IPlayerData playerData)
        {
            _windowsManager = windowsManager;
            _playerData = playerData;
        }

        protected override void Initialize()
        {
            AddSystem(new PlaceBetSystem(_playerData));
            AddSystem(new PlacedBetWindowSystem(_windowsManager));
            AddSystem(new PlayerSaveSystem(_playerData));

            AddSystem(new DisposeBetRequestSystem());
        }
    }
}