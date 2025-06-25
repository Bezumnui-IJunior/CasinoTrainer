using Windows;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class InfoWithCloseButtonWindow : Window
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private WindowsId _window;

        private IWindowsManager _windowsManager;

        protected override void Initialize() =>
            _closeButton.onClick.AddListener(OnClick);

        protected override void Deinitialize() =>
            _closeButton.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            _windowsManager.Close(_window);
        }

        [Inject]
        private void Construct(IWindowsManager windowsManager)
        {
            _windowsManager = windowsManager;
        }
    }
}