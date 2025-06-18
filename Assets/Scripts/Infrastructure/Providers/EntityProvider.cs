using Features.EntityViewFactory.Components;
using Scellecs.Morpeh;
using TriInspector;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Infrastructure.Providers
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EntityProvider : MonoBehaviour, IEntityProvider
    {
        private bool _isInit;
        [field: ShowInInspector] public Entity Entity { get; private set; }

        public void SetEntity(Entity entity)
        {
            if (_isInit)
                return;

            _isInit = true;
            Entity = entity;
            Entity.GetWorld().GetStash<ViewComponent>().Add(Entity).Value = this;

            foreach (IComponentsProvider provider in GetComponentsInChildren<IComponentsProvider>())
                provider.Initialize(Entity, Entity.GetWorld());
        }

        [ContextMenu("Destroy")]
        public void Destroy()
        {
            foreach (IComponentsProvider provider in GetComponentsInChildren<IComponentsProvider>())
                provider.Deinitialize();

            Entity.GetWorld().RemoveEntity(Entity);
            Destroy(gameObject);
        }
    }
}