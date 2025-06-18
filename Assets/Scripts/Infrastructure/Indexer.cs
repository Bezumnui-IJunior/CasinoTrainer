namespace Infrastructure
{
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class Indexer : IIndexer
    {
        private int _current = 0;

        public int Next() =>
            ++_current;
    }
}