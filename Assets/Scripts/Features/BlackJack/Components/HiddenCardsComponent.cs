using Scellecs.Morpeh;

namespace Features.BlackJack.Components
{
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct HiddenCardsComponent : IComponent
    {
        public int Value;
    }
}