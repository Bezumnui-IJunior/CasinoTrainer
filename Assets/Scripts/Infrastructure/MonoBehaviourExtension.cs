using UnityEngine;

namespace Infrastructure
{
    public static class MonoBehaviourExtension
    {
        public static void DoSelfInjection(this MonoBehaviour obj) =>
            GlobalInjector.Inject(obj);
    }
}