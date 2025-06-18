using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Progress
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class PersistantScriptableObject : ScriptableObject
    {
        private PersistantSerializer _serializer;
        protected abstract string PrefsKey { get; }

        private void OnEnable()
        {
            _serializer = new PersistantSerializer(PrefsKey);
        }

        public void Save() =>
            _serializer.Save(this);

        public bool Load<T>()=>
            _serializer.Load<T>(this);
    }
}