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
    public class ExitButton
    {
        private readonly Button _exitButton;
        private readonly WindowsId _windowId;
        private readonly IWindowsManager _windowsManager;
        private readonly ISettings _settings;

        public ExitButton(ISettings settings, IWindowsManager windowsManager, Button exitButton, WindowsId windowId)
        {
            _settings = settings;
            _windowsManager = windowsManager;
            _exitButton = exitButton;
            _windowId = windowId;
        }

        public void Enable() =>
            _exitButton.onClick.AddListener(OnExitClicked);

        public void Disable() =>
            _exitButton.onClick.RemoveListener(OnExitClicked);

        private void OnExitClicked()
        {
            _settings.Save();
            _windowsManager.Close(_windowId);
        }
    }
}