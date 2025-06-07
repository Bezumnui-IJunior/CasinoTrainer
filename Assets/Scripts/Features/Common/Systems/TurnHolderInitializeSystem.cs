using Features.BlackJack.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Common.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TurnHolderInitializeSystem : IInitializer
    {
        private Entity _entity;

        public World World { get; set; }

        public void OnAwake()
        {
            _entity = World.CreateEntity();
            World.GetStash<TurnHolderComponent>().Add(_entity);
        }

        public void Dispose()
        {
            World.RemoveEntity(_entity);
        }
    }
}