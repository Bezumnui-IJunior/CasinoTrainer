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
        private readonly IPlayerData _playerData;
        private Stash<BetComponent> _bet;
        private Filter _filter;
        private Stash<PlaceBetRequest> _request;

        public PlaceBetSystem(IPlayerData playerData)
        {
            _playerData = playerData;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<PlayerTag>()
                .Without<BetComponent>()
                .Build();

            _request = World.GetStash<PlaceBetRequest>();
            _bet = World.GetStash<BetComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (PlaceBetRequest bet in _request)
            foreach (Entity player in _filter)
            {
                int betValue = bet.Value;

                _playerData.ChargeMoney(betValue);
                _bet.Add(player).Value = betValue;
            }
        }

        public void Dispose() { }
    }
}