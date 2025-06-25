using Windows;
using Features.GameOver.Systems;
using Scellecs.Morpeh.Addons.Feature;
using Sounds.Configs;
using Unity.IL2CPP.CompilerServices;
using VContainer;
using View;

namespace Features.GameOver
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameOverFeature : CombinedFeature
    {
        private readonly IWindowsManager _windowsManager;
        private readonly ISoundFXService _soundFXService;
        private readonly IMusicConfig _musicConfig;

        [Inject]
        public GameOverFeature(IWindowsManager windowsManager, ISoundFXService soundFXService, IMusicConfig musicConfig)
        {
            _windowsManager = windowsManager;
            _soundFXService = soundFXService;
            _musicConfig = musicConfig;
        }

        protected override void Initialize()
        {
            AddSystem(new GameOverWindowSystem(_windowsManager));
            AddSystem(new GameOverDrawSystem(_windowsManager));
            AddSystem(new GameOverPlaySoundSystem(_soundFXService, _musicConfig));
            AddSystem(new GameOverTurnSystem());
            AddSystem(new WinMoneySystem());
            AddSystem(new DrawMoneySystem());
        }
    }
}