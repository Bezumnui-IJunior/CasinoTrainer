using System;
using Windows;
using GameStates;
using Infrastructure;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace View.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        private IStateMachine _stateMachine;
        private IWindowsManager _windowsManager;

        [Inject]
        protected void Constructor(IStateMachine stateMachine, IWindowsManager windowsManager)
        {
            _stateMachine = stateMachine;
            _windowsManager = windowsManager;
        }

        private void Awake()
        {
            this.DoSelfInjection();
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayClicked);
            _settingsButton.onClick.AddListener(OnSettingsClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsClicked);
        }

        private void OnPlayClicked()
        {
            _stateMachine.ChangeState<BlackJackStartState>();
            _playButton.interactable = false;
        }

        private void OnSettingsClicked()
        {
            _windowsManager.Open(WindowsId.SettingsWindow);
        }
    }
}