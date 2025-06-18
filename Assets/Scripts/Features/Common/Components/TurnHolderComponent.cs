using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Common.Components
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct TurnHolderComponent : IComponent
    {
        public int Value;
    }
}