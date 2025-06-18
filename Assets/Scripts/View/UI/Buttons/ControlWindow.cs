using Windows;
using GameStates;
using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Common.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ControlWindow : Window
    {
        [SerializeField] private Button _exitButton;
        private IStateMachine _stateMachine;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _exitButton.onClick.AddListener(OnClicked);
        }

        [Inject]
        private void Constructor(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void OnClicked()
        {
            _stateMachine.ChangeState<MainMenuState>();
            _exitButton.interactable = false;
        }
    }
}