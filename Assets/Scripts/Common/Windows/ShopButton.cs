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
    public class ShopButton : Window
    {
        [SerializeField] private Button _exitButtonUI;
        private ExitButton _exitButton;

        [Inject]
        private void Construct(ISettings settings, IWindowsManager windowsManager)
        {
            _exitButton = new ExitButton(settings, windowsManager, _exitButtonUI, WindowsId.ShopWindow);
        }

        private void OnEnable()
        {
            _exitButton.Enable();
        }

        private void OnDisable()
        {
            _exitButton.Disable();
        }
        
    }
}