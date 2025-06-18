using Windows;
using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace View.UI.Buttons
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ControlWindow : Window
    {
        [SerializeField] private Button _exitButton;
        private GoToMenuButton _goToMenuButton;

        public void OnEnable() =>
            _goToMenuButton.Enable();

        private void OnDisable() =>
            _goToMenuButton.Disable();

        [Inject]
        private void Constructor(IStateMachine stateMachine)
        {
            _goToMenuButton = new GoToMenuButton(stateMachine, _exitButton);
        }
    }
}