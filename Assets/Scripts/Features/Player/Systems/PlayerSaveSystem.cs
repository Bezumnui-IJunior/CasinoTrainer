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
    public class PlayerSaveSystem : ICleanupSystem
    {
        private readonly IPlayerData _playerData;
        private Filter _filter;
        private Stash<MoneyHolderComponent> _moneyHolder;
        private Stash<SaveRequestTag> _saveRequest;

        public PlayerSaveSystem(IPlayerData playerData)
        {
            _playerData = playerData;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<PlayerTag>()
                .With<MoneyHolderComponent>()
                .With<SaveRequestTag>()
                .Build();

            _moneyHolder = World.GetStash<MoneyHolderComponent>();
            _saveRequest = World.GetStash<SaveRequestTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity player in _filter)
            {
                ref float balance = ref _moneyHolder.Get(player).Value;
                _playerData.PlayerMoney = balance;
                _playerData.Save();
                _saveRequest.Remove(player);
            }
        }

        public void Dispose() { }
    }
}