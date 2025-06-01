using Infrastructure.Providers;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.EntityViewFactory.Components
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ViewComponent : IComponent
    {
        public EntityProvider Value;
    }
}