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
    public class SettingsButton : Window
    {
        private const float DebugAddMoneyValue = 100;
        [SerializeField] private Button _exitButtonUI;
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Slider _musicSliderUI;

        private ExitButton _exitButton;
        private MusicSlider _musicSlider;
        private IPlayerData _playerData;

        private void OnEnable()
        {
            _addMoneyButton.onClick.AddListener(OnAddMoneyClicked);
            _musicSlider.Enable();
            _exitButton.Enable();
        }

        private void OnDisable()
        {
            _addMoneyButton.onClick.RemoveListener(OnAddMoneyClicked);
            _musicSlider.Disable();
            _exitButton.Disable();
        }

        protected override void OnUpdate()
        {
            _musicSlider.Update();
        }

        [Inject]
        private void Construct(ISettings settings, IWindowsManager windowsManager, IPlayerData playerData, IBackgroundMusic backgroundMusic)
        {
            _playerData = playerData;
            _musicSlider = new MusicSlider(_musicSliderUI, settings, backgroundMusic);
            _exitButton = new ExitButton(settings, windowsManager, _exitButtonUI, WindowsId.SettingsWindow);
        }

        private void OnAddMoneyClicked()
        {
            _playerData.AddMoney(DebugAddMoneyValue);
            _playerData.Save();
        }
    }
}