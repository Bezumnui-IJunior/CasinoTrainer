using System;
using Features.BlackJack.Components;
using Features.View.Components;
using Progress;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Player.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlaceBetSystem : ICleanupSystem
    {
        private Stash<BetComponent> _bet;
        private Filter _filter;
        private Stash<PlaceBetRequest> _request;
        private Stash<MoneyHolderComponent> _moneyHolder;
        private Stash<SaveRequestTag> _saveRequest;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<PlayerTag>()
                .With<MoneyHolderComponent>()
                .Without<BetComponent>()
                .Without<SaveRequestTag>()
                .Build();

            _request = World.GetStash<PlaceBetRequest>();
            _bet = World.GetStash<BetComponent>();
            _moneyHolder = World.GetStash<MoneyHolderComponent>();
            _saveRequest = World.GetStash<SaveRequestTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (PlaceBetRequest bet in _request)
            foreach (Entity player in _filter)
            {
                ref float balance = ref _moneyHolder.Get(player).Value;
                int betValue = bet.Value;
                
                if (betValue < 0 || betValue > balance)
                    throw new InvalidOperationException($"Invalid charge: {betValue}. Should be validated before.");

                balance -= betValue;

                _bet.Add(player).Value = betValue;
                _saveRequest.Add(player);
            }
        }
        
        public void Dispose() { }
    }
}