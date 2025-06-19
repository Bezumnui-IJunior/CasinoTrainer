using Windows;
using Common.Settings;
using Progress;
using Sounds;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class SettingsWindow : Window
    {
        private const float DebugAddMoneyValue = 100;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Slider _musicSliderUI;
        
        private MusicSlider _musicSlider;
        private IPlayerData _playerData;
        private ISettings _settings;
        private IWindowsManager _windowsManager;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitClicked);
            _addMoneyButton.onClick.AddListener(OnAddMoneyClicked);
            _musicSlider.Enable();
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitClicked);
            _addMoneyButton.onClick.RemoveListener(OnAddMoneyClicked);
            _musicSlider.Disable();
        }

        protected override void OnUpdate()
        {
            _musicSlider.Update();
        }

        [Inject]
        private void Construct(ISettings settings, IWindowsManager windowsManager, IPlayerData playerData, IBackgroundMusic backgroundMusic)
        {
            _settings = settings;
            _windowsManager = windowsManager;
            _playerData = playerData;
            _musicSlider = new MusicSlider(_musicSliderUI, settings, backgroundMusic);
        }

        private void OnExitClicked()
        {
            _settings.Save();
            _windowsManager.Close(WindowsId.SettingsWindow);
        }

        private void OnAddMoneyClicked()
        {
            _playerData.AddMoney(DebugAddMoneyValue);
            _playerData.Save();
        }
    }
}