using UnityEngine;

namespace Infrastructure
{
    public interface IInstantilizer
    {
        T Instantiate<T>(T prefab)  where T : Component ;
        T Instantiate<T>(T prefab, Transform parent, bool worldPositionStays = false) where T : Component;
    }
}