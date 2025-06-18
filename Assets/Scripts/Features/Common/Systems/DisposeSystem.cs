using Features.Common.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Common.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DisposeSystem : ICleanupSystem
    {
        private Filter _filter;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<DisposingTag>()
                .Build();

        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
                World.RemoveEntity(entity);
        }

        public void Dispose() { }
    }
}