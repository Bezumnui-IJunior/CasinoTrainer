using Features.EntityViewFactory.Components;
using Infrastructure.Providers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.EntityViewFactory.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EntityViewPathFactorySystem : ISystem
    {
        private Filter _filter;
        private Stash<ViewPathComponent> _viewPath;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<ViewPathComponent>()
                .Without<ViewComponent>()
                .Build();

            _viewPath = World.GetStash<ViewPathComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref ViewPathComponent path = ref _viewPath.Get(entity);
                Object.Instantiate(Resources.Load<EntityProvider>(path.Value)).SetEntity(entity);
            }
        }

        public void Dispose() { }
    }
}