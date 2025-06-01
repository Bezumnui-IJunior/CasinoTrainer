using Features.EntityViewFactory.Components;
using JetBrains.Annotations;
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
        [ShowInInspector] private Entity _entity;
        private bool _isInit;

        [ReadOnly] public int EntityId { get; private set; }

        public void SetEntity(Entity entity)
        {
            if (_isInit)
                return;

            _isInit = true;
            _entity = entity;
            EntityId = _entity.Id;
            _entity.GetWorld().GetStash<ViewComponent>().Add(_entity).Value = this;
            

            foreach (IComponentsProvider registrar in GetComponentsInChildren<IComponentsProvider>())
                registrar.Initialize(_entity, _entity.GetWorld());
        }

        [ContextMenu("Destroy")]
        public void Destroy()
        {
            _entity.GetWorld().RemoveEntity(_entity);
            Destroy(gameObject);
        }
    }
}