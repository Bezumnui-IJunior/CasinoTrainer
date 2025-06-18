using Features.Common.Components;
using Features.EntityViewFactory.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Common.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class DisposeViewSystem : ICleanupSystem
    {
        private Stash<ViewComponent> _view;
        private Filter _filter;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<DisposingTag>()
                .With<ViewComponent>()
                .Build();

            _view = World.GetStash<ViewComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                _view.Get(entity).Value.Destroy();
                World.RemoveEntity(entity);
            }
        }

        public void Dispose() { }
    }
}