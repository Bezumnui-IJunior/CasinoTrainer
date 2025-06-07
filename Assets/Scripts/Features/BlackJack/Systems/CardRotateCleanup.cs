using Features.BlackJack.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.BlackJack.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CardRotateCleanup : ICleanupSystem
    {
        private Filter _filter;
        private Stash<RotateTag> _rotateTag;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<RotateTag>()
                .Build();

            _rotateTag = World.GetStash<RotateTag>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
                _rotateTag.Remove(entity);
        }

        public void Dispose() { }
    }
}