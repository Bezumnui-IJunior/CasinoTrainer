using Features.Common.Components;
using Features.EntityViewFactory.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Common.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class ChangePositionSystem : ISystem
    {
        private Stash<ChangePositionComponent> _changePositionComponent;
        private Filter _filter;
        private Stash<ViewComponent> _view;
        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<ChangePositionComponent>()
                .With<ViewComponent>()
                .Build();

            _view = World.GetStash<ViewComponent>();
            _changePositionComponent = World.GetStash<ChangePositionComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                _view.Get(entity).Value.transform.position = _changePositionComponent.Get(entity).Value;
                _changePositionComponent.Remove(entity);
            }
        }

        public void Dispose() { }
    }
}