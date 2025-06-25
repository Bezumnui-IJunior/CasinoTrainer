using Windows;
using Common.Settings;
using Progress;
using Sounds;
using Unity.IL2CPP.CompilerServices;
using Unity.Services.LevelPlay;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class SettingsButton : Window
    {
        private const float DebugAddMoneyValue = 100;
        [SerializeField] private ExitButton _exitButton;
        [SerializeField] private Button _debugButton;
        [SerializeField] private Slider _musicSliderUI;

        private MusicSlider _musicSlider;
        private IPlayerData _playerData;

        private void OnEnable()
        {
            _debugButton.onClick.AddListener(OnDebugClicked);
            _musicSlider.Enable();
        }

        private void OnDisable()
        {
            _debugButton.onClick.RemoveListener(OnDebugClicked);
            _musicSlider.Disable();
        }

        protected override void OnUpdate()
        {
            _musicSlider.Update();
        }

        [Inject]
        private void Construct(ISettings settings, IPlayerData playerData, IBackgroundMusic backgroundMusic, IObjectResolver resolver)
        {
            _playerData = playerData;
            _musicSlider = new MusicSlider(_musicSliderUI, settings, backgroundMusic);
            resolver.Inject(_exitButton);
        }

        private void OnDebugClicked()
        {
            LevelPlay.LaunchTestSuite();
        }
    }
}