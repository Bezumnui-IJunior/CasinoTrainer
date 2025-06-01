using System;
using Scellecs.Morpeh;
using TriInspector;
using View.Services;

namespace Features.View.Components
{
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CollectAnimationComponent : IComponent
    {
        public ICollectAnimation Value;
    }
}