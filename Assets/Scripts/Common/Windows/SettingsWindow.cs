using Windows;
using Progress;
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
        private IPlayerData _playerData;
        private ISettings _settings;
        private IWindowsManager _windowsManager;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitClicked);
            _addMoneyButton.onClick.AddListener(OnAddMoneyClicked);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitClicked);
            _addMoneyButton.onClick.RemoveListener(OnAddMoneyClicked);
        }

        [Inject]
        private void Construct(ISettings settings, IWindowsManager windowsManager, IPlayerData playerData)
        {
            _settings = settings;
            _windowsManager = windowsManager;
            _playerData = playerData;
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