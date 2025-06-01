using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.View.Components
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CardTakenEvent : IEventData
    {
        public Entity Entity;

        public CardTakenEvent(Entity entity)
        {
            Entity = entity;
        }
    }
}