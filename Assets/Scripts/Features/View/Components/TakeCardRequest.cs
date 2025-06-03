using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.View.Components
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct TakeCardRequest : IRequestData
    {
        public Entity Requestee;
        public readonly bool HasRequestee;
        public readonly bool HideCard;

        public TakeCardRequest(Entity requestee, bool hideCard)
        {
            Requestee = requestee;
            HasRequestee = true;
            HideCard = hideCard;
        }
    }
}