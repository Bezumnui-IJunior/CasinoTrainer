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
        private static readonly Vector3 SpawnPosition = -new Vector3(9999, 9999, 9999);
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
                EntityProvider gameObject = Object.Instantiate(Resources.Load<EntityProvider>(path.Value));
                gameObject.SetEntity(entity);
                gameObject.transform.position = SpawnPosition;
            }
        }

        public void Dispose() { }
    }
}