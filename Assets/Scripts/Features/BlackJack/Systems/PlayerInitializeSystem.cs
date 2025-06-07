using Features.BlackJack.Components;
using Features.View.Components;
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
        private readonly ICollectAnimation _playerCollectAnimation;

        public PlayerInitializeSystem(ICollectAnimation playerCollectAnimation)
        {
            _playerCollectAnimation = playerCollectAnimation;
        }

        public World World { get; set; }

        public void OnAwake()
        {
            Entity entity = World.CreateEntity();

            World.GetStash<PlayerTag>().Add(entity);
            World.GetStash<CardHolderComponent>().Add(entity);
            World.GetStash<ScoreComponent>().Add(entity);
            World.GetStash<CollectAnimationComponent>().Add(entity).Value = _playerCollectAnimation;
        }

        public void Dispose() { }
    }
}