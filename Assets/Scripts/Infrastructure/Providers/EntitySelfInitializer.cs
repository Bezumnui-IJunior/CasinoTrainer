using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Infrastructure.Providers
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EntitySelfInitializer : MonoBehaviour
    {
        private void Awake()
        {
            EntityProvider provider = GetComponent<EntityProvider>();

            if (provider == null)
                throw new MissingComponentException($"{nameof(EntityProvider)} was not found on the object");
            
            Entity entity = World.Default.CreateEntity();
            provider.SetEntity(entity);
        }
    }
}