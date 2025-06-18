using Features.BlackJack.Services;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using View.Services;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerInitializeSystem : IInitializer
    {
        private readonly IPlayerCollectAnimation _playerCollectAnimation;
        private readonly IPlayerFactory _playerFactory;

        public PlayerInitializeSystem(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            _playerFactory.CreatePlayer();
        }

        public void Dispose() { }
    }
}