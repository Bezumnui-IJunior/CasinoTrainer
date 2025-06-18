using System;
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
        protected Entity Entity { get; private set; }

        protected World World { get; private set; }

        public void Initialize(Entity entity, World world)
        {
            Entity = entity;
            World = entity.GetWorld();
            OnInitialize();
        }

        protected abstract void OnInitialize();

        public ref T AddToStash<T>() where T : struct, IComponent =>
            ref World.GetStash<T>().Add(Entity);
    }
}