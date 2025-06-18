using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class Instantilizer : IInstantilizer
    {
        private readonly IObjectResolver _resolver;

        [Inject]
        public Instantilizer(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public T Instantiate<T>(T prefab) where T : Component =>
            _resolver.Instantiate(prefab, null);

        public T Instantiate<T>(T prefab, Transform parent, bool worldPositionStays = false) where T : Component =>
            _resolver.Instantiate(prefab, parent, worldPositionStays);
    }
}