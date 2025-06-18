using Unity.IL2CPP.CompilerServices;

namespace Progress
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class PersistantData
    {
        private readonly PersistantSerializer _serializer;

        protected PersistantData(string prefsKey)
        {
            _serializer = new PersistantSerializer(prefsKey);
        }

        public void Save() =>
            _serializer.Save(this);

        protected static bool TryLoad<T>(out T result, string prefsKey) where T : PersistantData =>
            new PersistantSerializer(prefsKey).TryLoad(out result);
    }
}