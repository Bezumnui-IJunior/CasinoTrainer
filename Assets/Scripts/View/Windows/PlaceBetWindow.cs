using System;
using Windows;
using Features.View.Components;
using Infrastructure;
using Progress;
using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using View.UI.Buttons;

namespace View.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlaceBetWindow : Window
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _exitButton;
        private Stash<PlaceBetRequest> _betRequest;
        private GoToMenuButton _goToMenuButton;
        private IPlayerData _playerData;
        private ISettings _settings;
        private World _world;

        [Inject]
        private void Construct(World world, ISettings settings, IPlayerData playerData, IStateMachine stateMachine)
        {
            _world = world;
            _settings = settings;
            _playerData = playerData;
            _goToMenuButton = new GoToMenuButton(stateMachine, _exitButton);
        }

        protected override void Initialize()
        {
            _betRequest = _world.GetStash<PlaceBetRequest>();
            _confirmButton.onClick.AddListener(OnButtonClicked);
            _inputField.text = _settings.DefaultBet.ToString();
            _goToMenuButton.Enable();
        }

        protected override void Deinitialize()
        {
            _confirmButton.onClick.RemoveListener(OnButtonClicked);
            _goToMenuButton.Disable();
        }

        private void OnButtonClicked()
        {
            if (_inputField.text.Length == 0)
                return;

            if (int.TryParse(_inputField.text, out int amount) == false)
                throw new InvalidOperationException("Cannot continue with invalid number");

            _settings.DefaultBet = amount;

            ApplyBet(amount);
        }

        private void ApplyBet(int amount)
        {
            if (amount < 0 || amount > _playerData.PlayerMoney)
                return;

            _betRequest.Add(_world.CreateEntity()).Value = amount;
        }
    }
}