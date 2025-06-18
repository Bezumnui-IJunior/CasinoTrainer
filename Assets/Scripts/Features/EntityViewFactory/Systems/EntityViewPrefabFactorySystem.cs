using Features.EntityViewFactory.Components;
using Infrastructure.Providers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.EntityViewFactory.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EntityViewPrefabFactorySystem : ISystem
    {
        private static readonly Vector3 SpawnPosition = -new Vector3(9999, 9999, 9999);
        private Filter _filter;
        private Stash<ViewPrefabComponent> _viewPrefab;

        public World World { get; set; }

        public void OnAwake()
        {
            _filter = World.Filter
                .With<ViewPrefabComponent>()
                .Without<ViewComponent>()
                .Build();

            _viewPrefab = World.GetStash<ViewPrefabComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref ViewPrefabComponent viewPrefabComponent = ref _viewPrefab.Get(entity);
                EntityProvider gameObject = Object.Instantiate(viewPrefabComponent.Value);
                gameObject.SetEntity(entity);
                gameObject.transform.position = SpawnPosition;
            }
        }

        public void Dispose() { }
    }
}