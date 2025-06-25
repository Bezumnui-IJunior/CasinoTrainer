using System;
using Windows;
using Progress;
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
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private WindowsId _windowId;
        [SerializeField] private Button _exitButton;

        private ISettings _settings;
        private IWindowsManager _windowsManager;

        private void Awake()
        {
            if (_exitButton == null)
                _exitButton = GetComponent<Button>();
        }

        public void OnEnable() =>
            _exitButton.onClick.AddListener(OnExitClicked);

        public void OnDisable() =>
            _exitButton.onClick.RemoveListener(OnExitClicked);

        [Inject]
        public void Constructor(ISettings settings, IWindowsManager windowsManager)
        {
            _settings = settings;
            _windowsManager = windowsManager;
        }

        private void OnExitClicked()
        {
            _settings.Save();
            _windowsManager.Close(_windowId);
        }
    }
}