using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Infrastructure.Providers
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class ComponentsProvider : MonoBehaviour, IComponentsProvider
    {
        private Entity _entity;
        private World _world;

        public void Initialize(Entity entity, World world)
        {
            _entity = entity;
            _world = entity.GetWorld();
            OnInitialize();
        }

        protected abstract void OnInitialize();

        public ref T AddToStash<T>() where T : struct, IComponent =>
            ref _world.GetStash<T>().Add(_entity);
    }
}