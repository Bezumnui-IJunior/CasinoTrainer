using System;
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
        [SerializeField] private Button _exitButton;
        private ISettings _settings;
        private IWindowsManager _windowsManager;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitClicked);
        }

        [Inject]
        private void Construct(ISettings settings, IWindowsManager windowsManager)
        {
            _settings = settings;
            _windowsManager = windowsManager;
        }

        private void OnExitClicked()
        {
            _settings.Save();
            _windowsManager.Close(WindowsId.SettingsWindow);
        }
    }
}
