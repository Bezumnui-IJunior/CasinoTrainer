using System;
using Windows;
using Features.BlackJack.Components;
using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace View.Windows
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlaceBetWindow : Window
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _confirmButton;
        private World _world;
        private Stash<BetComponent> _betStash;
        private Filter _playerFilter;

        [Inject]
        private void Construct(World world)
        {
            _world = world;
        }
        
        protected override void Initialize()
        {
            _playerFilter = _world.Filter
                .With<PlayerTag>()
                .Without<BetComponent>()
                .Build();
            
            _betStash = _world.GetStash<BetComponent>();
            _confirmButton.onClick.AddListener(OnButtonClicked);
        }

        protected override void Deinitialize()
        {
            _confirmButton.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            if (_inputField.text.Length == 0)
                return;

            if (int.TryParse(_inputField.text, out int amount) == false)
                throw new InvalidOperationException("Cannot continue with invalid number");
            
            ApplyBet(amount);
        }

        private void ApplyBet(int amount)
        {
            foreach (Entity player in _playerFilter)
                _betStash.Add(player).Value = amount;
        }
    }
}