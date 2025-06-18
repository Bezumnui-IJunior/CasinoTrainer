using Unity.IL2CPP.CompilerServices;

namespace Infrastructure
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class Indexer : IIndexer
    {
        private int _current;

        public int Next() =>
            ++_current;
    }
}