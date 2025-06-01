using Features.EntityViewFactory.Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Object = UnityEngine.Object;

namespace Features.EntityViewFactory.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EntityViewPrefabFactorySystem : ISystem
    {
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
                Object.Instantiate(viewPrefabComponent.Value).SetEntity(entity);
            }
        }

        public void Dispose() { }
    }
}